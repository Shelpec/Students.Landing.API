// Interfaces/ICompanyService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<Company?> GetByIdAsync(Guid id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> CreateAsync(Company entity);
        Task<Company?> UpdateAsync(Guid id, Company entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
