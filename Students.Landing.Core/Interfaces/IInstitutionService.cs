// Interfaces/IUniversityService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IInstitutionService
    {
        Task<Institution?> GetByIdAsync(Guid id);
        Task<IEnumerable<Institution>> GetAllAsync();
        Task<Institution> CreateAsync(Institution entity);
        Task<Institution?> UpdateAsync(Guid id, Institution entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
