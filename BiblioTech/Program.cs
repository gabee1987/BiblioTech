using BiblioTech.Domain.Repositories;
using BiblioTech.Infrastructure.Data;
using BiblioTech.Infrastructure.Repositories;
using BiblioTech.Services;
using BiblioTech.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<LibraryDbContext>( options => options.UseNpgsql( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );
builder.Services.AddLogging();
builder.Services.AddControllers();

// Application Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddAutoMapper( typeof( BiblioTech.Services.MappingProfiles.BookProfile ).Assembly );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
