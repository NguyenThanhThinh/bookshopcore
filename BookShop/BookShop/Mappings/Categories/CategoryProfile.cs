using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Categories;

namespace BookShop.Mappings.Categories
{
	public class CategoryProfile: Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CategoryViewModel>();
		}
		
	}
}
