using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {       
        }

        public SalesContext(DbContextOptions options)
            :base(options)
        {         
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);
                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.HasMany(e => e.Sales)
                    .WithOne(s => s.Product)
                    .HasForeignKey(s => s.ProductId);
            });

            builder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasMaxLength(80);

                entity.HasMany(e => e.Sales)
                    .WithOne(s => s.Customer)
                    .HasForeignKey(s => s.CustomerId);

            });

            builder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(80);

                entity.HasMany(e => e.Sales)
                    .WithOne(s => s.Store)
                    .HasForeignKey(s => s.StoreId);
            });

            builder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();
            });

        }
    }
}
