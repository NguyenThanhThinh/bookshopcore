using BookShop.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Accounts
{
    public class AccountCreateUpdateViewModel : BaseViewModel<Guid>
    {

        [Required(ErrorMessage = ErrorMessageRequired)]
        [Display(Name = "Họ và tên")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = ErrorMessageStringLength)]
        public string FullName { get; set; }
        [Required(ErrorMessage = ErrorMessageRequired)]        
        [Display(Name = "Tên đăng nhập")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessageStringLength)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ErrorMessageRequired)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessageRequired)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = ErrorMessageStringLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không trùng")]
        public string ConfirmPassword { get; set; }

        public List<CheckboxRoleViewModel> RolesList { get; set; } = new List<CheckboxRoleViewModel>();

    }
}