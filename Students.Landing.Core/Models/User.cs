using System;
using System.Collections.Generic;

namespace Students.Landing.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        // 🔹 Навигационное свойство: все заявки студента
        public List<Application> Applications { get; set; } = new();
    }
}
