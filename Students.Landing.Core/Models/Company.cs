using System;
using System.Collections.Generic;

namespace Students.Landing.Core.Models
{
    /// <summary>
    /// Компания (организация), принимающая практикантов.
    /// </summary>
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;

        // Многие компании имеют список "CompanyDirection", где указано сколько мест
        // по каждому направлению есть.
        public List<CompanyDirection> CompanyDirections { get; set; } = new();
    }
}
