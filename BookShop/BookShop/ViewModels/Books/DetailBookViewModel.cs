namespace BookShop.ViewModels.Books
{
	public class DetailBookViewModel : BaseViewModel<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public decimal PriceFormatted { get; set; }

		public string CategoryName { get; set; }

		public string AuthorName { get; set; }

		public string Image { get; set; }
	}
}
