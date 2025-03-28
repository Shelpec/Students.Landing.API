using Microsoft.EntityFrameworkCore;
using Students.Landing.Core.Data;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Landing.Infrastructure.Repositories
{
    public class StudentApplicationRepository : GenericRepository<StudentApplication>, IStudentApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentApplication>> GetPendingApplicationsForUniversity(Guid universityId)
        {
            return await _context.StudentApplications
                .Include(app => app.Student)
                    .ThenInclude(student => student.Major)
                    .ThenInclude(major => major.UniversityMajors) // Вместо `major.University`
                        .ThenInclude(um => um.University)
                .Include(app => app.CompanyDirection)
                    .ThenInclude(cd => cd.Company)
                .Where(app => !app.IsUniversityApproved && app.Status == ApplicationStatus.Pending)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentApplication>> GetPendingApplicationsForCompany(Guid companyId)
        {
            return await _context.StudentApplications
                .Include(app => app.Student)
                    .ThenInclude(student => student.Major)
                    .ThenInclude(major => major.UniversityMajors) // Вместо `major.University`
                        .ThenInclude(um => um.University)
                .Include(app => app.CompanyDirection)
                    .ThenInclude(cd => cd.Company)
                .Where(app => app.IsUniversityApproved && app.Status == ApplicationStatus.UniversityApproved && app.CompanyDirection.CompanyId == companyId)
                .ToListAsync();
        }

    }
}
