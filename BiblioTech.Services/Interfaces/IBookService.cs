using BiblioTech.DTO;

namespace BiblioTech.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> AddBookAsync( BookDTO bookDTO );
        Task<IEnumerable<BookDTO>> GetAllBookAsync();
        Task<BookDTO> GetBookByIdAsync( int id );
        Task<BookDTO> UpdateBookAsync( BookDTO bookDTO );
        Task<BookDTO> DeleteBookAsync( int id );
    }
}
