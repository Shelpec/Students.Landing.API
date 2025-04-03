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
    public enum PracticeType
    {
        Production,            // 🔹 Производственная практика
        PreDiploma             // 🔹 Преддипломная практика
    }

    public class Application
    {
        public Guid Id { get; set; }

        // 🔹 Личные данные
        public string LastName { get; set; } = null!;        // Фамилия *
        public string FirstName { get; set; } = null!;       // Имя *
        public string? MiddleName { get; set; }              // Отчество
        public DateTime DateOfBirth { get; set; }            // Дата рождения *
        public string Gender { get; set; } = null!;          // Пол * (Мужской / Женский)

        // 🔹 Контактные данные
        public string PhoneNumber { get; set; } = null!;     // Телефон *
        public string Email { get; set; } = null!;           // Электронная почта *

        // 🔹 Образование
        public Guid InstitutionId { get; set; }               // Учебное заведение *
        public Institution? Institution { get; set; }

        public Guid MajorId { get; set; }                    // Специальность *
        public Major? Major { get; set; }

        public DateTime StartYear { get; set; }         // Год поступления *
        public DateTime EndYear { get; set; }         // Год окончания *
        public float GPA { get; set; }                       // GPA (текущий или итоговый) *

        // 🔹 Документы
        public string? PhotoUrl { get; set; }                // Фото *
        public string? StudentCardPhotoUrl { get; set; }     // Фото студенческого билета *

        // 🔹 Метаданные заявки
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        // 🔹 Практика 
        public PracticeType PracePracticeType { get; set; } = PracticeType.Production;     // Вид практики (производственная, преддипломная)
        public DateTime PracticeStart { get; set; }
        public DateTime PracticeEnd { get; set; }
        public string? Comment { get; set; }          // Комментарий к практике

        // 🔹 Дополнительные данные о студенте
        public string Languages { get; set; } = null!;        // Знание языков
        public string? Interests { get; set; }                // Личные интересы
        public string WorkExp { get; set; } = null!;   // Общий стаж работы
        public string Achievements { get; set; } = null!;     // Достижения, проекты и т.д.
        public string Motivation { get; set; } = null!;       // Мотивация

        // 🔹 Связи
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid? OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }

        public Guid PracticeFieldId { get; set; }
        public PracticeField? PracticeField { get; set; }
    }
}
