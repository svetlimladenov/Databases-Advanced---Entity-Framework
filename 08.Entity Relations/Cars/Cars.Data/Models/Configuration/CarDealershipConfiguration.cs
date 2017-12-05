using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Data.Models.Configuration
{
    public class CarDealershipConfiguration : IEntityTypeConfiguration<CarDealership>
    {
        public void Configure(EntityTypeBuilder<CarDealership> builder)
        {
            //Car Dealership
            builder
                .HasKey(cd => new
                    { cd.CarId, cd.DealershipId });

            builder
                .ToTable("CarsDealerships");

            builder
                .HasOne(cd => cd.Car)
                .WithMany(c => c.CarDealerships)
                .HasForeignKey(cd => cd.CarId);

            builder
                .HasOne(cd => cd.Dealership)
                .WithMany(d => d.CarDealerships)
                .HasForeignKey(cd => cd.DealershipId);
        }   
    }
}
