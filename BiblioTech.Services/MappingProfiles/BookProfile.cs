using AutoMapper;
using BiblioTech.Domain.Entities;
using BiblioTech.DTO;

namespace BiblioTech.Services.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();

        }
    }
}
