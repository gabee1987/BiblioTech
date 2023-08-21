using BiblioTech.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BiblioTech.Domain.Repositories;
using BiblioTech.Infrastructure.Repositories;
using BiblioTech.Services.Interfaces;
using BiblioTech.Services;
using BiblioTech.Services.MappingProfiles;

namespace BiblioTech.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void RegisterInfrastructureServices( this IServiceCollection services, string connectionString )
        {
            services.AddDbContext<LibraryDbContext>( options => options.UseNpgsql( connectionString ) );
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddAutoMapper( typeof( BookProfile ) );
        }
    }
}
