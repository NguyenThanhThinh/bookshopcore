using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mappings.Books
{
	public class BookProfile : Profile
	{
		public BookProfile()
		{
			CreateMap<Book, BookIndexViewModel>().
				ForMember(n => n.CategoryName, t => t.MapFrom(n => n.Category.Name))
				.ForMember(n => n.AuthorName, t => t.MapFrom(n => n.Author.Name));
			CreateMap<Book, CreateOrUpdateBookViewModel>().ForMember(n => n.PriceFormatted, t => t.MapFrom(n => n.Price));
			CreateMap<CreateOrUpdateBookViewModel,Book>().ForMember(n => n.Price, t => t.MapFrom(n => n.PriceFormatted));
		}
	}
}
