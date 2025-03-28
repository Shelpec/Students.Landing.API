namespace Students.Landing.Core.Models.DTOs
{
    public class CompanyDirectionDTO
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SpecializationDirectionId { get; set; }
        public int Capacity { get; set; }
        public int Used { get; set; }
    }
}
