using BiblioTech.Domain.Entities;

namespace BiblioTech.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> AddAsync( Book book );
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync( int id );
        Task<Book> UpdateAsync( Book book );
        Task<bool> DeleteAsync( int id );
        Task<IEnumerable<Book>> SearchBooksAsync( string query );
        Task<IEnumerable<Book>> SearchBooksByTitle( string title );
        Task<IEnumerable<Book>> SearchBooksByAuthorAsync( string author );
        Task<IEnumerable<Book>> SearchBooksByGenreAsync( string genre );
    }
}
