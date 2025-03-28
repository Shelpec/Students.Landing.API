// Interfaces/IStudentService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> CreateAsync(Student entity);
        Task<Student?> UpdateAsync(Guid id, Student entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
