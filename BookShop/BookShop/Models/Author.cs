using System;
using System.Collections.Generic;

namespace BookShop.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Alias { set; get; }
		public DateTime CreateDate { get; set; } = DateTime.Now;

		public List<Book> Books = new List<Book>();
	}
}
