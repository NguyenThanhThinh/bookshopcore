using System;
using System.Collections.Generic;

namespace BookShop.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreateDate { get; set; }

		public List<Book> Books = new List<Book>();
	}
}
