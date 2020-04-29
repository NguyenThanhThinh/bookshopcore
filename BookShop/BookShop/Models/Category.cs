using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please input your name")]
		public string Name { get; set; }

		public string Alias { set; get; }
		public string Description { get; set; }

		public DateTime CreateDate { get; set; } = DateTime.Now;

		public DateTime UpdateDate { get; set; } = DateTime.Now;

		public List<Book> Books = new List<Book>();
	}
}
