using System;
using System.ComponentModel;

namespace BookShop.ViewModels
{
	public class BaseViewModel
	{
		public int Id { get; set; }

		[DisplayName("Ngày tạo")]
		public DateTime CreateDate { get; set; } = DateTime.Now;

		public DateTime UpdateDate { get; set; } = DateTime.Now;
	}
}
