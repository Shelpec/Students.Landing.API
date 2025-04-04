﻿using System;
using System.Collections.Generic;

namespace Students.Landing.Core.Models
{
    /// <summary>
    /// Направление, 
    /// например "Информационные технологии",
    /// "Медицина", "Педагогика" и т.п.
    /// </summary>
    public class PracticeField
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        // Привязка к специальностям (Major)
        public List<Major> Majors { get; set; } = new();
    }
}
