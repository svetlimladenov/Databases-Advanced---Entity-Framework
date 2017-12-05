using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.HasOne(pm => pm.BankAccount)
                .WithMany(ba => ba.PaymentMethods)
                .HasForeignKey(pm => pm.BankAccountId);

            builder.HasOne(pm => pm.CreditCard)
                .WithMany(cc => cc.PaymentMethods)
                .HasForeignKey(pm => pm.CreditCardId);

            builder.HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(pm => pm.UserId);

            builder.Property(e => e.BankAccountId)
                .IsRequired(false);

            builder.Property(e => e.CreditCardId)
                .IsRequired(false);
        }
    }
}
