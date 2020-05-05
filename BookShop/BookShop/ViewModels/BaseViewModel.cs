using System;

namespace BookShop.ViewModels
{
	public class BaseViewModel
	{
		public int Id { get; set; }

		public DateTime CreateDate { get; set; } = DateTime.Now;

		public DateTime UpdateDate { get; set; } = DateTime.Now;
	}
}
