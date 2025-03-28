// Interfaces/IMajorService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IMajorService
    {
        Task<Major?> GetByIdAsync(Guid id);
        Task<IEnumerable<Major>> GetAllAsync();
        Task<Major> CreateOrAssignMajorToUniversity(Major entity, Guid universityId);
        Task<Major?> UpdateAsync(Guid id, Major entity, Guid universityId);
        Task<bool> DeleteAsync(Guid id);
    }
}
