using BiblioTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<LibraryDbContext>( options => options.UseNpgsql( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

builder.Services.AddControllers();
builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AutoMapper
builder.Services.AddAutoMapper( typeof( BiblioTech.Services.MappingProfiles.BookProfile ).Assembly );

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
