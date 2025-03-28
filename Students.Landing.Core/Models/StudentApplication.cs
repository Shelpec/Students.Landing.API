using System;

namespace Students.Landing.Core.Models
{
    public enum ApplicationStatus
    {
        Pending,                // 🔹 Ожидание подтверждения университетом
        UniversityApproved,     // 🔹 Университет подтвердил заявку
        RejectedByUniversity,   // 🔹 Университет отказал в заявке
        AcceptedByCompany,      // 🔹 Компания приняла заявку
        RejectedByCompany,      // 🔹 Компания отказала студенту
        InProgress,             // 🔹 Студент проходит практику
        Completed,              // 🔹 Практика завершена
        Cancelled               // 🔹 Заявка отменена студентом
    }

    public class StudentApplication
    {
        public Guid Id { get; set; }

        // 🔹 Метаданные заявки
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public bool IsUniversityApproved { get; set; }

        // 🔹 Практика
        public string PracticeType { get; set; } = null!;     // Вид практики (производственная, преддипломная)
        public DateTime PracticeStart { get; set; }
        public DateTime PracticeEnd { get; set; }
        public string? PracticeComment { get; set; }          // Комментарий к практике

        // 🔹 Дополнительные данные о студенте
        public string Languages { get; set; } = null!;        // Знание языков
        public string? Interests { get; set; }                // Личные интересы
        public string WorkExperience { get; set; } = null!;   // Общий стаж работы
        public string Achievements { get; set; } = null!;     // Достижения, проекты и т.д.
        public string Motivation { get; set; } = null!;       // Мотивация

        // 🔹 Связи
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }

        public Guid CompanyDirectionId { get; set; }
        public CompanyDirection? CompanyDirection { get; set; }
    }
}
