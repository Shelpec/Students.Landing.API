using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class CompanyDirectionService : ICompanyDirectionService
    {
        private readonly IGenericRepository<CompanyDirection> _repo;

        public CompanyDirectionService(IGenericRepository<CompanyDirection> repo)
        {
            _repo = repo;
        }

        public async Task<CompanyDirection?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<CompanyDirection>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<CompanyDirection> CreateAsync(CompanyDirection entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<CompanyDirection?> UpdateAsync(Guid id, CompanyDirection entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.CompanyId = entity.CompanyId;
            existing.SpecializationDirectionId = entity.SpecializationDirectionId;
            existing.Capacity = entity.Capacity;
            existing.Used = entity.Used;

            _repo.Update(existing);
            await _repo.SaveAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _repo.Delete(existing);
            await _repo.SaveAsync();
            return true;
        }
    }
}
