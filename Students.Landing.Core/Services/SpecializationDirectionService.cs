using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class SpecializationDirectionService : ISpecializationDirectionService
    {
        private readonly IGenericRepository<SpecializationDirection> _repo;

        public SpecializationDirectionService(IGenericRepository<SpecializationDirection> repo)
        {
            _repo = repo;
        }

        public async Task<SpecializationDirection?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<SpecializationDirection>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<SpecializationDirection> CreateAsync(SpecializationDirection entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<SpecializationDirection?> UpdateAsync(Guid id, SpecializationDirection entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Title = entity.Title;

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
