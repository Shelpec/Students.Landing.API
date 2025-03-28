// Interfaces/IStudentApplicationService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;  
using Students.Landing.Core.Models.DTOs;

namespace Students.Landing.Core.Interfaces
{
    public interface IStudentApplicationService
    {
        Task<StudentApplication?> GetByIdAsync(Guid id);
        Task<IEnumerable<StudentApplication>> GetAllAsync();
        Task<StudentApplication> CreateApplication(StudentApplication app);

        Task<StudentApplication?> ApproveByUniversity(Guid applicationId);
        Task<StudentApplication?> RejectByUniversity(Guid applicationId);
        Task<StudentApplication?> AcceptByCompany(Guid applicationId);
        Task<StudentApplication?> RejectByCompany(Guid applicationId);
        Task<StudentApplication?> MarkAsCompleted(Guid applicationId);
        Task<StudentApplication?> CancelApplication(Guid applicationId);
        Task<IEnumerable<CompanyDTO?>> GetAvailableCompaniesForStudent(Guid studentId);
        Task<IEnumerable<StudentApplicationDTO>> GetPendingApplicationsForUniversity(Guid universityId);
        Task<IEnumerable<StudentApplicationDTO>> GetPendingApplicationsForCompany(Guid companyId);

        Task<bool> DeleteAsync(Guid id);
    }
}
