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

		public const string ErrorMessageRequired = "{0} không được trống";
		public const string ErrorMessageMaxLength = "{0} phải nhỏ hơn {1} ký tự";
		public const string ErrorMessageStringLength = "{0} phải từ {2} tới {1} ký tự";
	}
}
