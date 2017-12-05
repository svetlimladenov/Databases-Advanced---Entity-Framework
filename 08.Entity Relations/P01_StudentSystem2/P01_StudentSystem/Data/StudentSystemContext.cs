using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=DESKTOP-D8U60HB\SQLEXPRESS;Database=StudentSystem;Integrated Security=True");
            }        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsUnicode(true)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsUnicode(false)
                    .HasMaxLength(10)
                    .IsRequired(false);

                entity.Property(e => e.RegisteredOn)
                    .IsRequired(true)
                    .HasColumnType("DATETIME2");

                entity.Property(e => e.Birthday)
                    .IsRequired(false)
                    .HasColumnType("DATETIME2");
                //
                //entity.HasMany(s => s.HomeworkSubmissions)
                //    .WithOne(h => h.Student)
                //    .HasForeignKey(h => h.StudentId);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(80);

                entity.Property(e => e.Description)
                    .IsRequired(false)
                    .IsUnicode();

                entity.Property(e => e.EndDate)
                    .HasColumnType("DATETIME2");

                //entity.HasMany(c => c.Resources)
                //    .WithOne(r => r.Course)
                //    .HasForeignKey(r => r.CourseId);

                //entity.HasMany(c => c.HomeworkSubmissions)
                //    .WithOne(h => h.Course)
                //    .HasForeignKey(h => h.CourseId);
            });

            builder.Entity<StudentCourse>()
                .HasKey(sd => new {sd.StudentId, sd.CourseId});

            builder.Entity<StudentCourse>()
                .HasOne(st => st.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(st => st.StudentId);

            builder.Entity<StudentCourse>()
                .HasOne(st => st.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(st => st.CourseId);

            builder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsUnicode(false)
                    .IsRequired();

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(e => e.CourseId);
            });

            builder.Entity<Homework>(entity =>
            {
                entity.HasKey(e => e.HomeworkId);

                entity.Property(e => e.Content)
                    .IsUnicode(false);

                entity.Property(e => e.SubmissionTime)
                    .HasColumnType("DATETIME2");

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(e => e.CourseId);
            });
        }
    }
}
