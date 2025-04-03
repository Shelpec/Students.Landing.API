// Interfaces/ISpecializationDirectionService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IPracticeFieldService
    {
        Task<PracticeField?> GetByIdAsync(Guid id);
        Task<IEnumerable<PracticeField>> GetAllAsync();
        Task<PracticeField> CreateAsync(PracticeField entity);
        Task<PracticeField?> UpdateAsync(Guid id, PracticeField entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
