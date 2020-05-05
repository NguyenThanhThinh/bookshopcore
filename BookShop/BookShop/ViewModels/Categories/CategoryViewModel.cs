using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Categories
{
	public class CategoryViewModel : BaseViewModel
	{

		[Required(ErrorMessage = "Please input your name")]
		public string Name { get; set; }

		public string Alias { set; get; }
		public string Description { get; set; }

	}
}
