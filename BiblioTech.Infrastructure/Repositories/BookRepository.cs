using BiblioTech.Domain.Entities;
using BiblioTech.Domain.Repositories;

namespace BiblioTech.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task<bool> DeleteAsync( int id )
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetByIdAsync( int id )
        {
            throw new NotImplementedException();
        }
    }
}
