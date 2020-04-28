using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable("Books");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn();

			builder.Property(x => x.Price).IsRequired();

			

		}
	}
}
