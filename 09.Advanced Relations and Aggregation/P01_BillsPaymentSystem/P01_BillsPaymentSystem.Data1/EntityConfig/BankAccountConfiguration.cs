﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(e => e.BankName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.SwiftCode)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}
