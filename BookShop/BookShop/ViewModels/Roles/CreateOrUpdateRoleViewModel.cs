using System;
using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Roles
{
	public class CreateOrUpdateRoleViewModel: BaseViewModel<Guid>
	{
	
		[Required(ErrorMessage = ErrorMessageRequired)]
		[Display(Name = "Tên vai trò")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessageStringLength)]
		public string Name { get; set; }

		public string Description { get; set; }
	}
}
