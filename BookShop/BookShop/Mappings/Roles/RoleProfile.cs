using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Roles;

namespace BookShop.Mappings.Roles
{
	public class RoleProfile : Profile
	{
		public RoleProfile()
		{
			CreateMap<AppRole, RoleIndexViewModel>();
			CreateMap<CreateOrUpdateRoleViewModel, AppRole>();
			CreateMap< AppRole, CreateOrUpdateRoleViewModel>();
			CreateMap<AppRole, CheckboxRoleViewModel>();
		}
	}
}
