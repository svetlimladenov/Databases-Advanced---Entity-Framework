using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            //•	A Town can be placed in one Country and a Country can have many Towns
            builder.HasKey(c => c.CountryId);

            builder.HasMany(c => c.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(c => c.TownId);
        }
    }
}