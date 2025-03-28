
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class UniversityMajorService : IUniversityMajorService
    {
        private readonly IGenericRepository<UniversityMajor> _umRepo;
        private readonly IGenericRepository<University> _univRepo;
        private readonly IGenericRepository<Major> _majorRepo;

        public UniversityMajorService(
            IGenericRepository<UniversityMajor> umRepo,
            IGenericRepository<University> univRepo,
            IGenericRepository<Major> majorRepo)
        {
            _umRepo = umRepo;
            _univRepo = univRepo;
            _majorRepo = majorRepo;
        }

        public async Task<UniversityMajor> AddMajorToUniversity(Guid universityId, Guid majorId)
        {
            // Проверяем, что университет существует
            var univ = await _univRepo.GetByIdAsync(universityId);
            if (univ == null)
                throw new Exception("Университет не найден");

            // Проверяем, что Major существует
            var maj = await _majorRepo.GetByIdAsync(majorId);
            if (maj == null)
                throw new Exception("Специальность (Major) не найдена");

            // Создаем связь
            var newUM = new UniversityMajor
            {
                Id = Guid.NewGuid(),
                UniversityId = universityId,
                MajorId = majorId
            };
            await _umRepo.AddAsync(newUM);
            await _umRepo.SaveAsync();
            return newUM;
        }

        public async Task<bool> RemoveMajorFromUniversity(Guid universityMajorId)
        {
            var existing = await _umRepo.GetByIdAsync(universityMajorId);
            if (existing == null) return false;

            _umRepo.Delete(existing);
            await _umRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Major>> GetMajorsByUniversity(Guid universityId)
        {
            // Ищем все связи UniversityMajor
            // (Можно оптимизировать через .Include(), но покажем базовый пример)
            var allUM = await _umRepo.FindAll(um => um.UniversityId == universityId);

            // Собираем Id специальностей
            var majorIds = allUM.Select(um => um.MajorId).Distinct().ToList();

            // Грузим все Majors
            var allMajors = await _majorRepo.GetAllAsync();
            return allMajors.Where(m => majorIds.Contains(m.Id));
        }

        public async Task<IEnumerable<University>> GetUniversitiesByMajor(Guid majorId)
        {
            var allUM = await _umRepo.FindAll(um => um.MajorId == majorId);

            var univIds = allUM.Select(um => um.UniversityId).Distinct().ToList();

            var allUnivs = await _univRepo.GetAllAsync();
            return allUnivs.Where(u => univIds.Contains(u.Id));
        }
    }
}
