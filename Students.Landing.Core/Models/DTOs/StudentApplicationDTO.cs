using System;

namespace Students.Landing.Core.Models.DTOs
{
    public class StudentApplicationDTO
    {
        public Guid Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime PracticeStart { get; set; }
        public DateTime PracticeEnd { get; set; }
        public string Status { get; set; } = "Unknown"; // Защита от null
        public bool IsUniversityApproved { get; set; }

        // Информация о студенте
        public Guid StudentId { get; set; }
        public string StudentFullName { get; set; } = "Неизвестный студент";
        public string StudentUniversity { get; set; } = "Неизвестный университет";
        public string StudentSpecialization { get; set; } = "Неизвестная специальность";

        // Информация о компании
        public Guid CompanyDirectionId { get; set; }
        public string CompanyName { get; set; } = "Неизвестная компания";
    }
}
