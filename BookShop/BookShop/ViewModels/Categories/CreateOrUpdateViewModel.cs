using BookShop.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Categories
{
	public class CreateOrUpdateViewModel : BaseViewModel
	{
		[Required(ErrorMessage = "Please input your name")]
		public string Name { get; set; }

		public string Alias
		{
			get;set;
		}
		public string Description { get; set; }
	}
}
