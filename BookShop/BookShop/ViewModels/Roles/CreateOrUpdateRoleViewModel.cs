using System;
using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Roles
{
	public class CreateOrUpdateRoleViewModel
	{
		public const string ErrorMessageRequired = "{0} không được trống";
		public const string ErrorMessageMaxLength = "{0} phải nhỏ hơn {1} ký tự";
		public const string ErrorMessageStringLength = "{0} phải từ {2} tới {1} ký tự";
		public Guid Id { get; set; }
		[Required(ErrorMessage = ErrorMessageRequired)]
		[Display(Name = "Tên vai trò")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessageStringLength)]
		public string Name { get; set; }

		public string Description { get; set; }
	}
}
