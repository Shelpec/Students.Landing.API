// Interfaces/IStudentService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User entity);
        Task<User?> UpdateAsync(Guid id, User entity);
        Task<bool> DeleteAsync(Guid id);

    }
}
