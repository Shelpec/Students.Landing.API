using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IStudentApplicationRepository : IGenericRepository<StudentApplication>
    {
        Task<IEnumerable<StudentApplication>> GetPendingApplicationsForUniversity(Guid universityId);
        Task<IEnumerable<StudentApplication>> GetPendingApplicationsForCompany(Guid companyId);
    }
}
