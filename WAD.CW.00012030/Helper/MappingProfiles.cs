using AutoMapper;
using WAD.CW1._00012030.Dto;
using WAD.CW1._00012030.Models;

namespace WAD.CW1._00012030.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
        }
    }
}
