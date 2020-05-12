using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Accounts;

namespace BookShop.Mappings.Accounts
{
	public class UserProfile : Profile
	{
		public UserProfile() 
		{
			CreateMap<AccountCreateUpdateViewModel, AppUser>();
		}
	}
}
