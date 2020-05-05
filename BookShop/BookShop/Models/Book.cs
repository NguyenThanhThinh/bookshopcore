using System;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
	public class Book
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please input your title")]
		public string Title { get; set; }

		public string Description { get; set; }
		[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]
		public decimal Price { get; set; }

		public string Alias { set; get; }
		[Required(ErrorMessage = "Please select your author")]
		public int AuthorId { get; set; }

		public string Image { get; set; }
		public Author Author { get; set; }

		[Required(ErrorMessage = "Please select your category")]
		public int CategoryId { get; set; }

		public Category Category { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public DateTime UpdateDate { get; set; } = DateTime.Now;
	}
}
