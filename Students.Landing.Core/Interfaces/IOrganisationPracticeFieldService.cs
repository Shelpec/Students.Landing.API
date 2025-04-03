// Interfaces/ICompanyDirectionService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IOrganisationPracticeFieldService
    {
        Task<OrganisationPracticeField?> GetByIdAsync(Guid id);
        Task<IEnumerable<OrganisationPracticeField>> GetAllAsync();
        Task<OrganisationPracticeField> CreateAsync(OrganisationPracticeField entity);
        Task<OrganisationPracticeField?> UpdateAsync(Guid id, OrganisationPracticeField entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
