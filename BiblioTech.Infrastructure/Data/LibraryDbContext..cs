using BiblioTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiblioTech.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext( DbContextOptions<LibraryDbContext> options ) : base( options )
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
