using BookShop.Extensions;

namespace BookShop.ViewModels.Books
{
	public class BookIndexViewModel : BaseViewModel
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }
		public string PriceFormatted => Price.ToMoneyFormatted("đ");
		public string Alias { set; get; }

		public string CategoryName { get; set; }

		public string AuthorName { get; set; }
	}
}
