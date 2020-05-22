using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.ViewModels.Books
{
	public class DetailBookViewModel : BaseViewModel<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public decimal PriceFormatted { get; set; }

		public string CategoryName { get; set; }

		public string AuthorName { get; set; }
	}
}
