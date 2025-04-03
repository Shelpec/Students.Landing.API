using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.Landing.Core.Data;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Infrastructure.Repositories
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly ApplicationDbContext _context;

        public InstitutionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Institution?> GetByIdAsync(Guid id)
        {
            return await _context.Institutions
                .Include(u => u.InstitutionMajors)
                    .ThenInclude(um => um.Major)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Institution>> GetAllAsync()
        {
            return await _context.Institutions
                .Include(u => u.InstitutionMajors)
                    .ThenInclude(um => um.Major)
                .ToListAsync();
        }

        public async Task AddAsync(Institution institution)
        {
            await _context.Institutions.AddAsync(institution);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Institution institution)
        {
            _context.Institutions.Update(institution);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Institution institution)
        {
            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();
        }
    }
}
