namespace Students.Landing.Core.Models.DTOs
{
    public class MajorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Guid SpecializationDirectionId { get; set; }
        public Guid UniversityId { get; set; } // Добавлен параметр
    }
}
