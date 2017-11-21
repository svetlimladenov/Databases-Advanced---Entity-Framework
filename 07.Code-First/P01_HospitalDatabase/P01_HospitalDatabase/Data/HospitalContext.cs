using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
            
        }

        public HospitalContext(DbContextOptions options) 
            :base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                entity.Property(e => e.HasInsurance)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.VisitationId);

                entity.HasMany(e => e.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.DiagnoseId);
            });

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(100);

                
            });

            builder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                entity.HasOne(e => e.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(d => d.DoctorId);

                entity.Property(e => e.DoctorId)
                    .IsRequired(false);
            });

            builder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Comments)
                    .IsUnicode(true)
                    .HasMaxLength(250);
            });

            builder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                    .IsUnicode(true)
                    .HasMaxLength(50);
            });

            builder.Entity<PatientMedicament>()
                .ToTable("PatientsMedicaments")
                .HasKey(pm => new
                {
                    pm.PatientId,
                    pm.MedicamentId
                });
            builder.Entity<PatientMedicament>()
                .HasOne(e => e.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(e => e.MedicamentId);

            builder.Entity<PatientMedicament>()
                .HasOne(e => e.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(e => e.PatientId);

            

            
        }
    }
}
