using BookShop.Data.Configurations;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
	public class BookShopDbContext : DbContext
	{
		public BookShopDbContext(DbContextOptions options) : base(options)
		{
		}

		protected BookShopDbContext()
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new AuthorConfiguration());
			modelBuilder.ApplyConfiguration(new BookConfiguration());
		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<Author> Authors { get; set; }

		public DbSet<Book> Books { get; set; }
	}
}
