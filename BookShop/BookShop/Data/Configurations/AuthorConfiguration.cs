﻿using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations
{
	public class AuthorConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.ToTable("Authors");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id).UseIdentityColumn();
		}
	}
}
