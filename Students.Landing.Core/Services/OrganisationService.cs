using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IGenericRepository<Organisation> _repo;

        public OrganisationService(IGenericRepository<Organisation> repo)
        {
            _repo = repo;
        }

        public async Task<Organisation?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Organisation>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Organisation> CreateAsync(Organisation entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<Organisation?> UpdateAsync(Guid id, Organisation entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = entity.Name;
            existing.Description = entity.Description;
            existing.Address = entity.Address;
            existing.ContactPhone = entity.ContactPhone;
            existing.WebsiteUrl = entity.WebsiteUrl;

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
