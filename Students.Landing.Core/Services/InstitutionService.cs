using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository _repository;

        public InstitutionService(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Institution?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Institution>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Institution> CreateAsync(Institution entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public async Task<Institution?> UpdateAsync(Guid id, Institution entity)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = entity.Name;
            existing.Address = entity.Address;

            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing);
            return true;
        }
    }
}
