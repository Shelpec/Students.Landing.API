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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Host=localhost;Port=5432;Database=studentsLendingDb2;Username=postgres;Password=erbol2008",
        //            b => b.MigrationsAssembly("Students.Landing.Infrastructure"));
        //    }
        //}

        public DbSet<Institution> Institutions { get; set; } = null!;
        public DbSet<Major> Majors { get; set; } = null!;
        public DbSet<InstitutionMajor> InstitutionMajors { get; set; } = null!;
        public DbSet<PracticeField> PracticeFields { get; set; } = null!;
        public DbSet<Organisation> Organisations { get; set; } = null!;
        public DbSet<OrganisationPracticeField> OrganisationPracticeFields { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Application> Applications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Убираем каскадное удаление
            modelBuilder.Entity<Application>()
                .HasOne(sa => sa.User)
                .WithMany(s => s.Applications)
                .HasForeignKey(sa => sa.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Application>()
                .HasOne(sa => sa.PracticeField)
                .WithMany()
                .HasForeignKey(sa => sa.PracticeFieldId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstitutionMajor>()
                .HasKey(um => new { um.InstitutionId, um.MajorId });

            modelBuilder.Entity<InstitutionMajor>()
                .HasOne(um => um.Institution)
                .WithMany(u => u.InstitutionMajors)
                .HasForeignKey(um => um.InstitutionId);

            modelBuilder.Entity<InstitutionMajor>()
                .HasOne(um => um.Major)
                .WithMany(m => m.InstitutionMajors)
                .HasForeignKey(um => um.MajorId);

        }

    }
}
