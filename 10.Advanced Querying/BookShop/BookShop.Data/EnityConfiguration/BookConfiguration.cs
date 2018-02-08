using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EnityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>

    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(e => e.Title)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsUnicode()
                .HasMaxLength(1000);

            builder.Property(e => e.ReleaseDate)
                .IsRequired(false);

        }
    }
}
