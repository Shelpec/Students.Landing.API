using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IGenericRepository<Company> _repo;

        public CompanyService(IGenericRepository<Company> repo)
        {
            _repo = repo;
        }

        public async Task<Company?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Company>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Company> CreateAsync(Company entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<Company?> UpdateAsync(Guid id, Company entity)
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
