// Interfaces/IStudentApplicationService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;  
using Students.Landing.Core.Models.DTOs;

namespace Students.Landing.Core.Interfaces
{
    public interface IApplicationService
    {
        Task<Application?> GetByIdAsync(Guid id);
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application> CreateApplication(Application app);
        Task<bool> DeleteAsync(Guid id);
    }
}
