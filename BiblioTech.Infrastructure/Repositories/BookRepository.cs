using BiblioTech.Domain.Entities;
using BiblioTech.Domain.Repositories;
using BiblioTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BiblioTech.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository( LibraryDbContext dbContext, ILogger<BookRepository> logger )
        {
            _dbContext = dbContext;
            _logger    = logger;
        }

        public async Task<Book> AddAsync( Book book )
        {
            if ( book == null )
            {
                _logger.LogError( "The book to add is null." );
                throw new ArgumentNullException( nameof(book) );
            }

            try
            {
                await _dbContext.Books.AddAsync( book );
                await _dbContext.SaveChangesAsync();
                return book;
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "An error occurred while adding the book with an ID of {BookId}.", book.Id );
                throw;
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Books.ToListAsync();
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "An error occured while fetching all books." );
                throw;
            }
        }

        public async Task<Book> GetByIdAsync( int id )
        {
            try
            {
                return await _dbContext.Books.FindAsync( id );
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "An error occured while fetching a book with the ID of {BookId}", id );
                throw;
            }
        }
        public async Task<Book> UpdateAsync( Book book )
        {
            if ( book == null )
            {
                _logger.LogError( "Attempted to update a null book." );
                throw new ArgumentNullException( nameof(book) );
            }

            try
            {
                _dbContext.Books.Update( book );
                await _dbContext.SaveChangesAsync();
                return book;
            }
            catch ( DbUpdateConcurrencyException ex ) when ( ex.Entries.Single().Entity is  Book )
            {
                _logger.LogError( ex, "Concurrency conflict while updating a book with an ID of {BookId}. The book was modified by another user.", book.Id );
                throw new InvalidOperationException( $"The book with an ID of {book.Id} was modified by another user.", ex );
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "An error occured while updating a book with an ID of {BookId}", book.Id );
                throw;
            }
        }

        public async Task<bool> DeleteAsync( int id )
        {
            try
            {
                var book = new Book { Id = id };
                _dbContext.Books.Attach( book );
                _dbContext.Books.Remove( book );
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch ( DbUpdateConcurrencyException ex ) when ( !_dbContext.Books.Any( book => book.Id == id ) )
            {
                _logger.LogError( ex, "Concurrency conflict while deleting a book with an ID of {BookId}. The book was deleted by another user.", id );
                return false; 
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "An error occured while deleting a book with an ID of {BookId}", id );
                throw;
            }
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync( string query )
        {
            string lowerQuery = query.ToLower();

            try
            {
                return await _dbContext.Books
                    .Include( b => b.Authors )
                    .Include( b => b.Genres )
                    .Where( b => b.Title.ToLower().Contains( lowerQuery )
                            || b.Subtitle != null && b.Subtitle.ToLower().Contains( lowerQuery )
                            || b.ISBN10 != null && b.ISBN10.ToLower().Contains( lowerQuery )
                            || b.ISBN13 != null && b.ISBN13.ToLower().Contains( lowerQuery )
                            || b.Description != null && b.Description.ToLower().Contains( lowerQuery )
                            || b.Publisher != null && b.Publisher.ToLower().Contains( lowerQuery )
                    ).ToListAsync();
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "An error occurred while searching for books with query {Query}.", query );
                throw;
            }
        }
    }
}
