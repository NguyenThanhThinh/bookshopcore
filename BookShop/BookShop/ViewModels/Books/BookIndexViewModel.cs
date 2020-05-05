using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.ViewModels.Books
{
	public class BookIndexViewModel : BaseViewModel
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public string Alias { set; get; }

		public string CategoryName { get; set; }

		public string AuthorName { get; set; }
	}
}
