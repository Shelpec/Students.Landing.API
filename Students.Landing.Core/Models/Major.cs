using System;
using System.Collections.Generic;

namespace Students.Landing.Core.Models
{
    public class Major
    {
        public Guid Id { get; set; }

        // Название специальности, например: "ВТиПО", "ИИ", "Информатика"
        public string Name { get; set; } = null!;

        // Привязка к направлению (IT, Медицина и т.д.)
        public Guid SpecializationDirectionId { get; set; }
        public SpecializationDirection? SpecializationDirection { get; set; }

        // Новый список связей "университет-специальность"
        public List<UniversityMajor> UniversityMajors { get; set; } = new();
    }
}
