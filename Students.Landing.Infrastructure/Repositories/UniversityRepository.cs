using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.Landing.Core.Data;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Infrastructure.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly ApplicationDbContext _context;

        public UniversityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<University?> GetByIdAsync(Guid id)
        {
            return await _context.Universities
                .Include(u => u.UniversityMajors)
                    .ThenInclude(um => um.Major)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<University>> GetAllAsync()
        {
            return await _context.Universities
                .Include(u => u.UniversityMajors)
                    .ThenInclude(um => um.Major)
                .ToListAsync();
        }

        public async Task AddAsync(University university)
        {
            await _context.Universities.AddAsync(university);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(University university)
        {
            _context.Universities.Update(university);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(University university)
        {
            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();
        }
    }
}
