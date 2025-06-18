using AutoMapper;
using DAL.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.BookCates,
                opt => opt.MapFrom(src => src.BookCates)).ReverseMap();

            CreateMap<BookCategory, BookCategoryViewModel>().
               ForMember(dest => dest.CategoryName,
               opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "Unknown"))
               .ReverseMap();
            CreateMap<ApplicationUser, UserRegisterModel>().ReverseMap();
            CreateMap<ApplicationUser, AccountViewModel>().ReverseMap();
            CreateMap<Cart,CartViewModel>().ReverseMap();
            CreateMap<CartDetail,CartDetailViewModel>()
                .ForMember(dest=>dest.BookName, 
                opt=>opt.MapFrom(src=>src.Book != null ? src.Book.Name :"Unknown"))
                .ReverseMap();
        }
    }
}
