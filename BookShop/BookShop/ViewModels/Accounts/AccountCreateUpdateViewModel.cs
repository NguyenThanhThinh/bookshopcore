using BookShop.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Accounts
{
    public class AccountCreateUpdateViewModel 
    {
        public Guid Id { get; set; }
        
        public const string ErrorMessageRequired = "{0} không được trống";
        public const string ErrorMessageMaxLength = "{0} phải nhỏ hơn {1} ký tự";
        public const string ErrorMessageStringLength = "{0} phải từ {2} tới {1} ký tự";

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