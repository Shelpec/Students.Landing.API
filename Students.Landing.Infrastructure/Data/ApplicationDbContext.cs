using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Students.Landing.Core.Models;
using Students.Landing.Core.Data;

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



        // 📦 Добавляем метод для предзаполнения базы
        // 📦 Добавляем метод для предзаполнения базы
        /*public async Task SeedDataAsync()
        {
            // ❗ Очищаем только Applications перед пересозданием
            Applications.RemoveRange(Applications);
            await SaveChangesAsync();

            // ❗ ВНИМАНИЕ: Больше ничего не трогаем! 
            // Университеты, компании, студенты остаются такими, как есть.

            // 👉 Загружаем существующие объекты из базы
            var institution1 = await Institutions.FirstOrDefaultAsync();
            var major1 = await Majors.FirstOrDefaultAsync();
            var organisation1 = await Organisations.FirstOrDefaultAsync();
            var practiceField1 = await PracticeFields.FirstOrDefaultAsync();
            var user1 = await Users.FirstOrDefaultAsync();

            var institution2 = await Institutions.Skip(1).FirstOrDefaultAsync();
            var major2 = await Majors.Skip(1).FirstOrDefaultAsync();
            var organisation2 = await Organisations.Skip(1).FirstOrDefaultAsync();
            var practiceField2 = await PracticeFields.Skip(1).FirstOrDefaultAsync();
            var user2 = await Users.Skip(1).FirstOrDefaultAsync();

            if (institution1 == null || major1 == null || organisation1 == null || practiceField1 == null || user1 == null)
            {
                Console.WriteLine("❌ Ошибка: Нет данных для создания первой заявки.");
                return;
            }

            if (institution2 == null || major2 == null || organisation2 == null || practiceField2 == null || user2 == null)
            {
                Console.WriteLine("❌ Ошибка: Нет данных для создания второй заявки.");
                return;
            }

            // 🔹 Создаем заявки
            var application1 = new Application
            {
                Id = Guid.NewGuid(),
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович",
                DateOfBirth = new DateTime(2002, 5, 12),
                Gender = "Мужской",
                PhoneNumber = "+7 777 123 4567",
                Email = "ivanov@example.com",
                InstitutionId = institution1.Id,
                MajorId = major1.Id,
                StartYear = new DateTime(2020, 9, 1),
                EndYear = new DateTime(2024, 6, 30),
                GPA = 3.5f,
                PhotoUrl = null,
                StudentCardPhotoUrl = null,
                SubmittedAt = DateTime.UtcNow,
                Status = ApplicationStatus.Pending,
                PracePracticeType = PracticeType.Production,
                PracticeStart = new DateTime(2024, 7, 1),
                PracticeEnd = new DateTime(2024, 9, 1),
                Comment = "Очень мотивирован пройти практику.",
                Languages = "Русский, Английский",
                Interests = "Программирование, ИИ",
                WorkExp = "1 год",
                Achievements = "Победитель хакатона",
                Motivation = "Хочу развиваться в сфере IT",
                UserId = user1.Id,
                OrganisationId = organisation1.Id,
                PracticeFieldId = practiceField1.Id
            };

            var application2 = new Application
            {
                Id = Guid.NewGuid(),
                LastName = "Петрова",
                FirstName = "Анна",
                MiddleName = "Сергеевна",
                DateOfBirth = new DateTime(2001, 3, 8),
                Gender = "Женский",
                PhoneNumber = "+7 777 987 6543",
                Email = "petrova@example.com",
                InstitutionId = institution2.Id,
                MajorId = major2.Id,
                StartYear = new DateTime(2019, 9, 1),
                EndYear = new DateTime(2023, 6, 30),
                GPA = 3.8f,
                PhotoUrl = null,
                StudentCardPhotoUrl = null,
                SubmittedAt = DateTime.UtcNow,
                Status = ApplicationStatus.Pending,
                PracePracticeType = PracticeType.PreDiploma,
                PracticeStart = new DateTime(2024, 5, 1),
                PracticeEnd = new DateTime(2024, 7, 1),
                Comment = "Ищу практику в медицинском центре.",
                Languages = "Русский, Английский",
                Interests = "Медицина, Исследования",
                WorkExp = "2 года",
                Achievements = "Лучший студент года",
                Motivation = "Мечтаю стать хирургом",
                UserId = user2.Id,
                OrganisationId = organisation2.Id,
                PracticeFieldId = practiceField2.Id
            };

            try
            {
                await Applications.AddRangeAsync(application1, application2);
                await SaveChangesAsync();
                Console.WriteLine("✅ Заявки успешно добавлены!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Ошибка при создании заявок: " + ex.Message);
            }
        }*/



    }
}
