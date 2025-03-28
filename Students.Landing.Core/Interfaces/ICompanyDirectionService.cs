// Interfaces/ICompanyDirectionService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface ICompanyDirectionService
    {
        Task<CompanyDirection?> GetByIdAsync(Guid id);
        Task<IEnumerable<CompanyDirection>> GetAllAsync();
        Task<CompanyDirection> CreateAsync(CompanyDirection entity);
        Task<CompanyDirection?> UpdateAsync(Guid id, CompanyDirection entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
