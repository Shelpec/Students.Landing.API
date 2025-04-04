﻿using System;
using System.Collections.Generic;

namespace Students.Landing.Core.Models
{
    public class Major
    {
        public Guid Id { get; set; }

        // Название специальности, например: "ВТиПО", "ИИ", "Информатика"
        public string Name { get; set; } = null!;

        public Guid PracticeFieldId { get; set; }
        public PracticeField? PracticeField { get; set; }

        // Новый список связей "университет-специальность"
        public List<InstitutionMajor> InstitutionMajors { get; set; } = new();
    }
}
