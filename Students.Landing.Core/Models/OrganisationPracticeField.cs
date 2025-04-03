using System;

namespace Students.Landing.Core.Models
{
    /// <summary>
    /// Таблица-связка: "Компания" <-> "Направление"
    /// + поля "Capacity" (сколько всего мест по этому направлению)
    /// + "Used" (сколько уже занято).
    /// </summary>
    public class OrganisationPracticeField
    {
        public Guid Id { get; set; }

        public Guid OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }

        public Guid PracticeFieldId { get; set; }
        public PracticeField? PracticeField { get; set; }

        public int Capacity { get; set; }    // Сколько всего мест
        public int Used { get; set; }       // Сколько мест занято
    }
}
