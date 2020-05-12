using System;
using System.ComponentModel;

namespace BookShop.ViewModels.Users
{
	public class UserIndexViewModel
	{
		public string Id { get; set; }

		[DisplayName("Tài khoản")]
		public string UserName { get; set; }

		public string Email { get; set; }

		[DisplayName("Họ và tên")]
		public string FullName { get; set; }

	}
}
