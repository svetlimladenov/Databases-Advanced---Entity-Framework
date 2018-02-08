using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EnityConfiguration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
         
                builder.Property(e => e.FirstName)
                    .IsRequired(false)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                builder.Property(e => e.LastName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                builder.HasMany(at => at.Books)
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId);
            
        }
    }
}
