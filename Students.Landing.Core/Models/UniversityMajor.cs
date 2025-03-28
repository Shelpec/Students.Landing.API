using System;

namespace Students.Landing.Core.Models
{
    /// <summary>
    /// Промежуточная сущность (many-to-many) между University и Major.
    /// </summary>
    public class UniversityMajor
    {
        public Guid Id { get; set; }

        public Guid UniversityId { get; set; }
        public University? University { get; set; }

        public Guid MajorId { get; set; }
        public Major? Major { get; set; }
    }
}
