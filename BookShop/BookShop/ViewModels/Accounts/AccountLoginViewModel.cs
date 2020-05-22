using System;
using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Accounts
{
    public class AccountLoginViewModel : BaseViewModel<Guid>
    {
        [Required(ErrorMessage = ErrorMessageRequired)]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMessageStringLength)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ErrorMessageRequired)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = ErrorMessageStringLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ tài khoản")]
        public bool RememberMe { get; set; }
    }
}
