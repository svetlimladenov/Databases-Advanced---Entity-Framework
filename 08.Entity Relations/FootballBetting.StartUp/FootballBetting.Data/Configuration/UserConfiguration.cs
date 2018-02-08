using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.UserId);

            builder.Property(e => e.Username)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.Password)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsUnicode()
                .IsRequired();
        }
    }
}