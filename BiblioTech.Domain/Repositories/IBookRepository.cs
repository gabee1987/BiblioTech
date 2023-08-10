using BiblioTech.Domain.Entities;

namespace BiblioTech.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetByIdAsync(int id);
        Task<bool> DeleteAsync( int id );
    }
}
