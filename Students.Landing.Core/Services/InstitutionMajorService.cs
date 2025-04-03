
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class InstitutionMajorService : IInstitutionMajorService
    {
        private readonly IGenericRepository<InstitutionMajor> _umRepo;
        private readonly IGenericRepository<Institution> _univRepo;
        private readonly IGenericRepository<Major> _majorRepo;

        public InstitutionMajorService(
            IGenericRepository<InstitutionMajor> umRepo,
            IGenericRepository<Institution> univRepo,
            IGenericRepository<Major> majorRepo)
        {
            _umRepo = umRepo;
            _univRepo = univRepo;
            _majorRepo = majorRepo;
        }

        public async Task<InstitutionMajor> AddMajorToInstitution(Guid institutionId, Guid majorId)
        {
            // Проверяем, что университет существует
            var univ = await _univRepo.GetByIdAsync(institutionId);
            if (univ == null)
                throw new Exception("Университет не найден");

            // Проверяем, что Major существует
            var maj = await _majorRepo.GetByIdAsync(majorId);
            if (maj == null)
                throw new Exception("Специальность (Major) не найдена");

            // Создаем связь
            var newUM = new InstitutionMajor
            {
                Id = Guid.NewGuid(),
                InstitutionId = institutionId,
                MajorId = majorId
            };
            await _umRepo.AddAsync(newUM);
            await _umRepo.SaveAsync();
            return newUM;
        }

        public async Task<bool> RemoveMajorFromInstitution(Guid institutionMajorId)
        {
            var existing = await _umRepo.GetByIdAsync(institutionMajorId);
            if (existing == null) return false;

            _umRepo.Delete(existing);
            await _umRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Major>> GetMajorsByInstitution(Guid institutionId)
        {
            // Ищем все связи UniversityMajor
            // (Можно оптимизировать через .Include(), но покажем базовый пример)
            var allUM = await _umRepo.FindAll(um => um.InstitutionId == institutionId);

            // Собираем Id специальностей
            var majorIds = allUM.Select(um => um.MajorId).Distinct().ToList();

            // Грузим все Majors
            var allMajors = await _majorRepo.GetAllAsync();
            return allMajors.Where(m => majorIds.Contains(m.Id));
        }

        public async Task<IEnumerable<Institution>> GetInstitutionsByMajor(Guid majorId)
        {
            var allUM = await _umRepo.FindAll(um => um.MajorId == majorId);

            var univIds = allUM.Select(um => um.InstitutionId).Distinct().ToList();

            var allUnivs = await _univRepo.GetAllAsync();
            return allUnivs.Where(u => univIds.Contains(u.Id));
        }
    }
}
