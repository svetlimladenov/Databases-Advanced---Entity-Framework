using PetClinic.Models;

namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<Vet> Vets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProcedureAnimalAid>()
                .HasKey(pc => new {pc.AnimalAidId, pc.ProcedureId});

            builder.Entity<ProcedureAnimalAid>()
                .HasOne(e => e.Procedure)
                .WithMany(p => p.ProcedureAnimalAids)
                .HasForeignKey(e => e.ProcedureId);

            builder.Entity<ProcedureAnimalAid>()
                .HasOne(e => e.AnimalAid)
                .WithMany(a => a.AnimalAidProcedures)
                .HasForeignKey(e => e.AnimalAidId);

            //builder.Entity<Vet>()
            //    .HasAlternateKey(v => v.PhoneNumber);

            builder.Entity<AnimalAid>()
                .HasAlternateKey(aa => aa.Name);

            builder.Entity<Animal>()
                .HasOne(a => a.Passport)
                .WithOne(p => p.Animal)
                .HasForeignKey<Animal>(a => a.PassportSerialNumber);

            builder.Entity<Passport>()
                .HasKey(a => a.SerialNumber);

            builder.Entity<Procedure>()
                .Ignore(e => e.Cost);

            builder.Entity<Procedure>()
                .Ignore(e => e.Cost);
        }
    }
}
