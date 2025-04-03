using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IInstitutionMajorService
    {
        Task<InstitutionMajor> AddMajorToInstitution(Guid institutionId, Guid majorId);
        Task<bool> RemoveMajorFromInstitution(Guid institutionMajorId);

        Task<IEnumerable<Major>> GetMajorsByInstitution(Guid institutionId);
        Task<IEnumerable<Institution>> GetInstitutionsByMajor(Guid majorId);
    }
}
