using System;
using System.Collections.Generic;

namespace BookShop.Models
{
	public class Category
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime CreateDate { get; set; }

		public List<Book> Books = new List<Book>();
	}
}
