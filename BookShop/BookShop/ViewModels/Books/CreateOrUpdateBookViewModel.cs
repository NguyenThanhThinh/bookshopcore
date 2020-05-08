namespace BookShop.ViewModels.Books
{
	public class CreateOrUpdateBookViewModel:BaseViewModel
	{
		
		public string Title { get; set; }

		
		public string Description { get; set; }
		
		public decimal Price { get; set; }


		public string Alias { set; get; }

		public string PriceFormatted { get; set; }
		public int CategoryId { get; set; }


		public int AuthorId { get; set; }

		public string Image { get; set; }
	}
}
