using System;

namespace Students.Landing.Core.Models
{
    /// <summary>
    /// Таблица-связка: "Компания" <-> "Направление"
    /// + поля "Capacity" (сколько всего мест по этому направлению)
    /// + "Used" (сколько уже занято).
    /// </summary>
    public class CompanyDirection
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        public Guid SpecializationDirectionId { get; set; }
        public SpecializationDirection? SpecializationDirection { get; set; }

        public int Capacity { get; set; }    // Сколько всего мест
        public int Used { get; set; }       // Сколько мест занято
    }
}
