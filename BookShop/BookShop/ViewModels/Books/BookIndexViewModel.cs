using BookShop.Extensions;
using System.ComponentModel;

namespace BookShop.ViewModels.Books
{
	public class BookIndexViewModel : BaseViewModel<int>
	{
		[DisplayName("Tên sách")]
		public string Title { get; set; }

		[DisplayName("Mô tả")]
		public string Description { get; set; }
		[DisplayName("Giá Sách")]
		public decimal Price { get; set; }
		
		public string Image { get; set; }
		public string PriceFormatted => Price.ToMoneyFormatted("đ");
		public string Alias { set; get; }

		[DisplayName("Thể loại")]
		public string CategoryName { get; set; }

		[DisplayName("Tác giả")]
		public string AuthorName { get; set; }
	}
}
