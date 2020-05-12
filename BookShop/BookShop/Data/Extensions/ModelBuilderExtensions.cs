using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Extensions
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{

			// any guid
			var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
			var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
			modelBuilder.Entity<AppRole>().HasData(new AppRole
			{
				Id = roleId,
				Name = "admin",
				NormalizedName = "admin",
				Description = "Administrator role"
			});

			var hasher = new PasswordHasher<AppUser>();
			modelBuilder.Entity<AppUser>().HasData(new AppUser
			{
				Id = adminId,
				UserName = "admin",
				NormalizedUserName = "admin",
				Email = "thanhthinhcntt@gmail.com",
				NormalizedEmail = "thanhthinhcntt@gmail.com",
				EmailConfirmed = true,
				PasswordHash = hasher.HashPassword(null, "Thanh@123"),
				SecurityStamp = string.Empty,
				FirstName = "Thinh",
				LastName = "Thanh",
				FullName="Nguyen Thanh Thinh",
				Dob = new DateTime(2020, 01, 31)
			});

			modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
			{
				RoleId = roleId,
				UserId = adminId
			});
		}


	}
}
