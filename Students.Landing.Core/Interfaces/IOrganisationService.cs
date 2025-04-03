// Interfaces/ICompanyService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IOrganisationService
    {
        Task<Organisation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Organisation>> GetAllAsync();
        Task<Organisation> CreateAsync(Organisation entity);
        Task<Organisation?> UpdateAsync(Guid id, Organisation entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
