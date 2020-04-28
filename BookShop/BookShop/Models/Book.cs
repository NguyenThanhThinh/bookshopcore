using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
	public class Book
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public int AuthorId { get; set; }

		public Author Author { get; set; }

		public int CategoryId { get; set; }

		public Category Category { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
