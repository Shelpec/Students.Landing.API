using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class PracticeFieldService : IPracticeFieldService
    {
        private readonly IGenericRepository<PracticeField> _repo;

        public PracticeFieldService(IGenericRepository<PracticeField> repo)
        {
            _repo = repo;
        }

        public async Task<PracticeField?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<PracticeField>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<PracticeField> CreateAsync(PracticeField entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<PracticeField?> UpdateAsync(Guid id, PracticeField entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = entity.Name;

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
