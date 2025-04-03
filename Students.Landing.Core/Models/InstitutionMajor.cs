using System;

namespace Students.Landing.Core.Models
{
    /// <summary>
    /// Промежуточная сущность (many-to-many) 
    /// между University и Major.
    /// </summary>
    public class InstitutionMajor
    {
        public Guid Id { get; set; }

        public Guid InstitutionId { get; set; }
        public Institution? Institution { get; set; }

        public Guid MajorId { get; set; }
        public Major? Major { get; set; }
    }
}
