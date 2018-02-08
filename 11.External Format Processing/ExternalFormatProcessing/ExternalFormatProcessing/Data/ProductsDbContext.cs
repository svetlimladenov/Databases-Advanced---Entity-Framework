using Microsoft.EntityFrameworkCore;

namespace ExternalFormatProcessing.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext()
        {
            
        }

        public ProductsDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=DESKTOP-D8U60HB\SQLEXPRESS;Database=ExternalFormatProcessing;Integrated Security = True");
            }
        }
    }
}
