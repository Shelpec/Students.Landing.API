using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.Landing.Core.Services
{
    public class OrganisationPracticeFieldService : IOrganisationPracticeFieldService
    {
        private readonly IGenericRepository<OrganisationPracticeField> _repo;
        private readonly IGenericRepository<Major> _majorRepo;
        private readonly IOrganisationPracticeFieldRepository _orgPracticeFieldRepo; // <=== Новый репозиторий

        public OrganisationPracticeFieldService(
            IGenericRepository<OrganisationPracticeField> repo,
            IGenericRepository<Major> majorRepo,
            IOrganisationPracticeFieldRepository orgPracticeFieldRepo // <=== Добавляем в конструктор
        )
        {
            _repo = repo;
            _majorRepo = majorRepo;
            _orgPracticeFieldRepo = orgPracticeFieldRepo;
        }

        public async Task<OrganisationPracticeField?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<OrganisationPracticeField>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<OrganisationPracticeField> CreateAsync(OrganisationPracticeField entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<OrganisationPracticeField?> UpdateAsync(Guid id, OrganisationPracticeField entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.OrganisationId = entity.OrganisationId;
            existing.PracticeFieldId = entity.PracticeFieldId;
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

        // ➕ Новый метод через репозиторий
        public async Task<IEnumerable<OrganisationPracticeField>> GetOrgFieldsByMajorAsync(Guid majorId)
        {
            return await _orgPracticeFieldRepo.GetOrgFieldsByMajorAsync(majorId);
        }
    }
}
