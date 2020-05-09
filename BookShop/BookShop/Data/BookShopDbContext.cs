using BookShop.Data.Configurations;
using BookShop.Data.Extensions;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShop.Data
{
	public class BookShopDbContext : IdentityDbContext<AppUser, AppRole,Guid>
	{
		public BookShopDbContext(DbContextOptions options) : base(options)
		{
		}

	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new AuthorConfiguration());
			modelBuilder.ApplyConfiguration(new BookConfiguration());

			modelBuilder.ApplyConfiguration(new AppUserConfiguration());
			modelBuilder.ApplyConfiguration(new AppRoleConfiguration());

			modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
			modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
			modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

			modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
			modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

			modelBuilder.Seed();

		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<Author> Authors { get; set; }

		public DbSet<Book> Books { get; set; }
	}
}
