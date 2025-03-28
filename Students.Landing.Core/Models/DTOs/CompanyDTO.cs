namespace Students.Landing.Core.Models.DTOs
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Address { get; set; } = "";
        public string ContactPhone { get; set; } = "";
        public string WebsiteUrl { get; set; } = "";
    }
}
