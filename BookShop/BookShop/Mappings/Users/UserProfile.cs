using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mappings.Users
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<AppUser, UserIndexViewModel>();
		}
	}
}
