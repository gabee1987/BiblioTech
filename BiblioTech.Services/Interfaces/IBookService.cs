using BiblioTech.Domain.Entities;
using BiblioTech.DTO;

namespace BiblioTech.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> AddBookAsync( BookDTO bookDTO );
        Task<IEnumerable<BookDTO>> GetAllBookAsync();
        Task<BookDTO> GetBookByIdAsync( int id );
        Task<BookDTO> UpdateBookAsync( BookDTO bookDTO );
        Task<bool> DeleteBookAsync( int id );
        Task<IEnumerable<BookDTO>> SearchBooksAsync( string query  );
        Task<IEnumerable<BookDTO>> SearchBooksByTitle( string title );
        Task<IEnumerable<BookDTO>> SearchBooksByAuthorAsync( string author );
        Task<IEnumerable<BookDTO>> SearchBooksByGenreAsync( string genre );
    }
}
