using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data
{
	public class BookShopDbContextFactory : IDesignTimeDbContextFactory<BookShopDbContext>
	{
		

		BookShopDbContext IDesignTimeDbContextFactory<BookShopDbContext>.CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("BookShopDbContext");

			var optionsBuilder = new DbContextOptionsBuilder<BookShopDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new BookShopDbContext(optionsBuilder.Options);
		}
	}
}
