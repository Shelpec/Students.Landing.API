// Interfaces/IUniversityService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IUniversityService
    {
        Task<University?> GetByIdAsync(Guid id);
        Task<IEnumerable<University>> GetAllAsync();
        Task<University> CreateAsync(University entity);
        Task<University?> UpdateAsync(Guid id, University entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
