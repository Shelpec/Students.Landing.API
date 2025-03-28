using System;

namespace Students.Landing.Core.Models.DTOs;

public class StudentDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }
    public Guid MajorId { get; set; }
}
