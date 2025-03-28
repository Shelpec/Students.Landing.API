// Interfaces/ISpecializationDirectionService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface ISpecializationDirectionService
    {
        Task<SpecializationDirection?> GetByIdAsync(Guid id);
        Task<IEnumerable<SpecializationDirection>> GetAllAsync();
        Task<SpecializationDirection> CreateAsync(SpecializationDirection entity);
        Task<SpecializationDirection?> UpdateAsync(Guid id, SpecializationDirection entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
