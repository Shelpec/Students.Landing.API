using System;
using System.Collections.Generic;

namespace Students.Landing.Core.Models
{
    public class Student
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
        public Guid UniversityId { get; set; }               // Учебное заведение *
        public University? University { get; set; }

        public Guid MajorId { get; set; }                    // Специальность *
        public Major? Major { get; set; }

        public DateTime EnrollmentYear { get; set; }         // Год поступления *
        public DateTime GraduationYear { get; set; }         // Год окончания *
        public float GPA { get; set; }                       // GPA (текущий или итоговый) *

        // 🔹 Документы
        public string? PhotoUrl { get; set; }                // Фото *
        public string? StudentCardPhotoUrl { get; set; }     // Фото студенческого билета *

        // 🔹 Keycloak авторизация (если используется)
        public string? KeycloakUserId { get; set; }

        // 🔹 Навигационное свойство: все заявки студента
        public List<StudentApplication> StudentApplications { get; set; } = new();
    }
}
