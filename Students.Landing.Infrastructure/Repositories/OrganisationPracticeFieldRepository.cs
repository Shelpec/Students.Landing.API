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
    public class OrganisationPracticeFieldRepository : IOrganisationPracticeFieldRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganisationPracticeFieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrganisationPracticeField>> GetOrgFieldsByMajorAsync(Guid majorId)
        {
            var major = await _context.Majors.FirstOrDefaultAsync(m => m.Id == majorId);
            if (major == null) return new List<OrganisationPracticeField>();

            var practiceFieldId = major.PracticeFieldId;

            var result = await _context.OrganisationPracticeFields
                .Include(op => op.Organisation)
                .Include(op => op.PracticeField)
                .Where(op => op.PracticeFieldId == practiceFieldId)
                .ToListAsync();

            return result;
        }
    }
}
