using Refit;

namespace BiblioTech.DataPopulator
{
    public interface IGoogleBooksApi
    {
        [Get( "/books/v1/volumes?q=books&projection=full&printType=books&maxResults={maxResults}&startIndex={startIndex}&key={apiKey}" )]
        Task<GoogleBooksResponse> GetBooks( int startIndex, int maxResults, string apiKey );
    }
}
