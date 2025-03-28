using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IUniversityRepository
    {
        Task<University?> GetByIdAsync(Guid id);
        Task<IEnumerable<University>> GetAllAsync();
        Task AddAsync(University university);
        Task UpdateAsync(University university);
        Task DeleteAsync(University university);
    }
}
