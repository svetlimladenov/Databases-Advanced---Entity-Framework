using System;
using Cars.Data.Models;
using Cars.Data.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Cars.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext()
        {
            
        }

        public CarsDbContext(DbContextOptions options)
            :base(options)
        {
            
        }
        public DbSet<Make> Makes { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Dealership> Dealerships { get; set; }

        public DbSet<CarDealership> CarDealerships { get; set; }

        public DbSet<Engine> Engines { get; set; }                

        public DbSet<LicensePlate> LicensePlates { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=DESKTOP-D8U60HB\SQLEXPRESS;Database=Cars;Integrated Security=True");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new EngineConfiguration());
            builder.ApplyConfiguration(new CarDealershipConfiguration());

        }
    }
}
