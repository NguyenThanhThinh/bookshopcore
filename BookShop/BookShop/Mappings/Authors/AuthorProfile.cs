using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Authors;

namespace BookShop.Mappings.Authors
{
	public class AuthorProfile : Profile
	{
		public AuthorProfile()
		{
			CreateMap<Author, AuthorViewModel>();
			CreateMap<Author, CreateOrUpdateAuthorViewModel>();
			CreateMap<CreateOrUpdateAuthorViewModel, Author>();
		}
	}
}
