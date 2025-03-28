using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Interfaces
{
    public interface IUniversityMajorService
    {
        Task<UniversityMajor> AddMajorToUniversity(Guid universityId, Guid majorId);
        Task<bool> RemoveMajorFromUniversity(Guid universityMajorId);

        Task<IEnumerable<Major>> GetMajorsByUniversity(Guid universityId);
        Task<IEnumerable<University>> GetUniversitiesByMajor(Guid majorId);
    }
}
