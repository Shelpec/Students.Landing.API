using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Host=localhost;Port=5432;Database=studentsLendingDb;Username=postgres;Password=erbol2008",
                    b => b.MigrationsAssembly("Students.Landing.Infrastructure"));
            }
        }

        public DbSet<University> Universities { get; set; } = null!;
        public DbSet<Major> Majors { get; set; } = null!;
        public DbSet<UniversityMajor> UniversityMajors { get; set; } = null!;
        public DbSet<SpecializationDirection> SpecializationDirections { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CompanyDirection> CompanyDirections { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<StudentApplication> StudentApplications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Убираем каскадное удаление
            modelBuilder.Entity<StudentApplication>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.StudentApplications)
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentApplication>()
                .HasOne(sa => sa.CompanyDirection)
                .WithMany()
                .HasForeignKey(sa => sa.CompanyDirectionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UniversityMajor>()
                .HasKey(um => new { um.UniversityId, um.MajorId });

            modelBuilder.Entity<UniversityMajor>()
                .HasOne(um => um.University)
                .WithMany(u => u.UniversityMajors)
                .HasForeignKey(um => um.UniversityId);

            modelBuilder.Entity<UniversityMajor>()
                .HasOne(um => um.Major)
                .WithMany(m => m.UniversityMajors)
                .HasForeignKey(um => um.MajorId);

        }

    }
}
