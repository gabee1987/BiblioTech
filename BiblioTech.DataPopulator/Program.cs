﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BiblioTech.DTO;
using BiblioTech.Services.Interfaces;
using BiblioTech.Services;
using BiblioTech.Infrastructure;
using Refit;
using BiblioTech.DataPopulator;
using Microsoft.Extensions.Logging;

public partial class Program
{

    static string connString     = "Host=localhost;Database=bibliotech_db;Username=bibliotech_user;Password=postgres87";
    static string API_KEY        = Environment.GetEnvironmentVariable( "BiblioTech_GOOGLE_BOOKS_API_KEY" );
    static int numberOfBooks     = 500;
    static string googleBooksUri = "https://www.googleapis.com";

    public static async Task Main( string[] args )
    {
        using var host = CreateHostBuilder( args ).Build();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        WriteMessage( "Starting to populate the db with books from Google Books API...", ConsoleColor.White );

        await PopulateDatabaseAsync( host.Services, logger );

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder( string[] args ) =>
        Host.CreateDefaultBuilder( args )
            .ConfigureServices( ( _, services ) =>
            {
                services.RegisterInfrastructureServices( connString );
                services.AddRefitClient<IGoogleBooksApi>().ConfigureHttpClient( c => c.BaseAddress = new Uri( googleBooksUri ) );
                services.AddTransient<IBookService, BookService>();
            } )
            .ConfigureLogging( logging =>
            {
                logging.ClearProviders();
            } );

    private static async Task PopulateDatabaseAsync( IServiceProvider serviceProvider, ILogger logger )
    {
        var bookService    = serviceProvider.GetRequiredService<IBookService>();
        var googleBooksApi = serviceProvider.GetRequiredService<IGoogleBooksApi>();

        WriteMessage( "Fetching books from Google Books API...", ConsoleColor.White );
        logger.LogInformation( "Fetching books from Google Books API..." );

        var books = await FetchBooksFromAPIAsync( googleBooksApi, numberOfBooks, logger );

        if ( books == null || !books.Any() )
        {
            WriteMessage( "Failed to fetch books.", ConsoleColor.Red );
            return;
        }

        Console.WriteLine();
        WriteMessage( $"Found {books.Count} books.", ConsoleColor.Green );
        WriteMessage( $"Populating the database...", ConsoleColor.White );
        Console.WriteLine();

        logger.LogInformation( $"Found {books.Count} books." );

        foreach ( var bookDTO in books )
        {
            await bookService.AddBookAsync( bookDTO );
        }

        Console.WriteLine();
        WriteMessage( $"-------------------------------------------------------", ConsoleColor.Green );
        WriteMessage( $"Successfully added {books.Count} books to the database.", ConsoleColor.Green );
        WriteMessage( $"-------------------------------------------------------", ConsoleColor.Green );
        Console.WriteLine();

        logger.LogInformation( $"Successfully added {books.Count} books to the database." );
    }

    private static async Task<List<BookDTO>> FetchBooksFromAPIAsync( IGoogleBooksApi googleBooksApi, int numberOfBooks, ILogger logger )
    {
        var books  = new List<BookDTO>();

        // As the Google Books API allows a maximum of 40 books per request, 
        // let's fetch them in chunks
        int maxGoogleBooksApiResults = 40;
        int numberOfRequests         = (int)Math.Ceiling( (double)numberOfBooks / maxGoogleBooksApiResults );
        int fetchedBooksCount        = 0;

        for ( int i = 0; i < numberOfRequests; i++ )
        {
            try
            {
                // Using Refit to handle google books api call
                var googleBooksResponse = await googleBooksApi.GetBooks( i * maxGoogleBooksApiResults, maxGoogleBooksApiResults, API_KEY );

                books.AddRange( googleBooksResponse.Items.Select( item => new BookDTO
                {
                    Title          = item.VolumeInfo.Title,
                    Subtitle       = item.VolumeInfo.Subtitle,
                    Authors        = item.VolumeInfo.Authors?.Select( a => new AuthorDTO { Name = a } ).ToList(),
                    ISBN10         = item.VolumeInfo.IndustryIdentifiers?.FirstOrDefault( ii => string.Equals( ii.Type, "ISBN_10", StringComparison.OrdinalIgnoreCase ) )?.Identifier,
                    ISBN13         = item.VolumeInfo.IndustryIdentifiers?.FirstOrDefault( ii => string.Equals( ii.Type, "ISBN_13", StringComparison.OrdinalIgnoreCase ) )?.Identifier,
                    Publisher      = item.VolumeInfo.Publisher,
                    PublishDate    = DateTime.TryParse( item.VolumeInfo.PublishedDate, out var date ) ? date.Year : (int?)null,
                    Genres         = item.VolumeInfo.Categories?.Select( category => new GenreDTO { Name = category } ).ToList(),
                    Language       = item.VolumeInfo.Language,
                    Description    = item.VolumeInfo.Description,
                    CoverImagePath = item.VolumeInfo.ImageLinks?.Thumbnail,
                    Thumbnail      = item.VolumeInfo.ImageLinks?.SmallThumbnail
                } ) );

                fetchedBooksCount += googleBooksResponse.Items.Count;

                // Break if we've fetched the desired number of books
                if ( fetchedBooksCount >= numberOfBooks )
                    break;
            }
            catch ( Exception ex )
            {
                logger.LogError( ex, "There was an error while fetching books from google books API." );
                throw;
            }
        }

        return books;
    }

    private static void WriteMessage( string message, ConsoleColor color )
    {
        Console.ForegroundColor = color;
        Console.WriteLine( message );
        Console.ResetColor();
    }
}

public class GoogleBooksResponse
{
    public List<BookItem> Items { get; set; }
}

public class BookItem
{
    public VolumeInfo VolumeInfo { get; set; }
}

public class VolumeInfo
{
    public string GoogleBooks_Id { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public List<string> Authors { get; set; }
    public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
    public string Publisher { get; set; }
    public string PublishedDate { get; set; }
    public ImageLinks ImageLinks { get; set; }
    public string Language { get; set; }
    public List<string> Categories { get; set; }
    public string? Description { get; set; }
}

public class IndustryIdentifier
{
    public string Type { get; set; }
    public string Identifier { get; set; }
}

public class ImageLinks
{
    public string SmallThumbnail { get; set; }
    public string Thumbnail { get; set; }
}