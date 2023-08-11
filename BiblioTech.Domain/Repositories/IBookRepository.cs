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
    }
}
