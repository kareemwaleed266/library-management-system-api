using AutoMapper;
using Library.Data.Entites.BookEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.BookService.Dtos
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookResultDto,BookDto>().ReverseMap();
            CreateMap<BookResultDto,Book>().ReverseMap();


            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.PublisherName, options => options.MapFrom(src => src.PublisherName))
                .ReverseMap();
        }
    }
}
