using Microsoft.AspNetCore.Identity;
using System;

namespace BookShop.Models
{
	public class AppRole: IdentityRole<Guid>
	{
		public string Description { get; set; }
	}
}
