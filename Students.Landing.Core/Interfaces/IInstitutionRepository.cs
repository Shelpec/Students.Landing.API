using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IInstitutionRepository
    {
        Task<Institution?> GetByIdAsync(Guid id);
        Task<IEnumerable<Institution>> GetAllAsync();
        Task AddAsync(Institution institution);
        Task UpdateAsync(Institution institution);
        Task DeleteAsync(Institution institution);
    }
}
