using Microsoft.AspNetCore.Identity;
using System;

namespace BookShop.Models
{
	public class AppUser: IdentityUser<Guid>
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime Dob { get; set; }

	}
}
