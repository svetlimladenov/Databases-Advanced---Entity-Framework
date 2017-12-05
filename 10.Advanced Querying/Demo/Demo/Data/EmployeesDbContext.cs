using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class EmployeesDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(
                    @"Server=DESKTOP-D8U60HB\SQLEXPRESS;Database=DemoEmployees;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .Property(e => e.Salary)
                .IsConcurrencyToken();
        }
    }
}
