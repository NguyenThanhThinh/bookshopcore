using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Authors
{

	public class AuthorViewModel: BaseViewModel<int>
	{
		public string Name { get; set; }

		public string Alias { set; get; }
		public string Description { get; set; }
	}
}
