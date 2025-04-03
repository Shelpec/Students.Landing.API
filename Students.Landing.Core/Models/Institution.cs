using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Students.Landing.Core.Models
{
    public class Institution
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        
        public List<InstitutionMajor> InstitutionMajors { get; set; } = new();
    }
}
