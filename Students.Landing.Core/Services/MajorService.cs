using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class MajorService : IMajorService
    {
        private readonly IGenericRepository<Major> _majorRepo;
        private readonly IGenericRepository<UniversityMajor> _universityMajorRepo;

        public MajorService(IGenericRepository<Major> majorRepo, IGenericRepository<UniversityMajor> universityMajorRepo)
        {
            _majorRepo = majorRepo;
            _universityMajorRepo = universityMajorRepo;
        }

        public async Task<Major?> GetByIdAsync(Guid id)
            => await _majorRepo.GetByIdAsync(id);

        public async Task<IEnumerable<Major>> GetAllAsync()
            => await _majorRepo.GetAllAsync();

        /// <summary>
        /// Создание специальности и автоматическое добавление связи с университетом
        /// </summary>
        public async Task<Major> CreateOrAssignMajorToUniversity(Major entity, Guid universityId)
        {
            // Проверяем, существует ли уже такая специальность
            var existingMajor = (await _majorRepo.FindAll(m => m.Name == entity.Name)).FirstOrDefault();
            if (existingMajor != null)
            {
                // Если уже существует связь с этим университетом, ошибка
                var existingLink = (await _universityMajorRepo.FindAll(um => um.MajorId == existingMajor.Id && um.UniversityId == universityId)).FirstOrDefault();
                if (existingLink != null)
                {
                    throw new Exception("Эта специальность уже привязана к данному университету.");
                }

                // Создаем новую связь, если специальность уже существует
                return await AssignMajorToUniversity(existingMajor.Id, universityId);
            }

            // Создаем новую специальность
            await _majorRepo.AddAsync(entity);
            await _majorRepo.SaveAsync();

            // Привязываем к университету
            return await AssignMajorToUniversity(entity.Id, universityId);
        }

        /// <summary>
        /// Метод для явного установления связи между университетом и специальностью
        /// </summary>
        public async Task<Major> AssignMajorToUniversity(Guid majorId, Guid universityId)
        {
            // Проверяем, существует ли уже связь
            var existingLink = (await _universityMajorRepo.FindAll(um => um.MajorId == majorId && um.UniversityId == universityId)).FirstOrDefault();
            if (existingLink != null)
            {
                throw new Exception("Эта специальность уже привязана к данному университету.");
            }

            // Добавляем новую связь
            var universityMajor = new UniversityMajor
            {
                UniversityId = universityId,
                MajorId = majorId
            };
            await _universityMajorRepo.AddAsync(universityMajor);
            await _universityMajorRepo.SaveAsync();

            // Возвращаем специальность
            return await _majorRepo.GetByIdAsync(majorId) ?? throw new Exception("Ошибка при получении специальности после привязки.");
        }

        /// <summary>
        /// Обновление специальности и привязки к университету
        /// </summary>
        public async Task<Major?> UpdateAsync(Guid id, Major entity, Guid universityId)
        {
            var existing = await _majorRepo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = entity.Name;
            existing.SpecializationDirectionId = entity.SpecializationDirectionId;

            _majorRepo.Update(existing);
            await _majorRepo.SaveAsync();

            // Обновляем связь с университетом
            await AssignMajorToUniversity(id, universityId);

            return existing;
        }

        /// <summary>
        /// Удаление специальности с разрывом всех связей с университетами
        /// </summary>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _majorRepo.GetByIdAsync(id);
            if (existing == null) return false;

            // Удаляем все связи с университетами перед удалением специальности
            var universityMajors = await _universityMajorRepo.FindAll(um => um.MajorId == id);
            foreach (var um in universityMajors)
            {
                _universityMajorRepo.Delete(um);
            }
            await _universityMajorRepo.SaveAsync();

            _majorRepo.Delete(existing);
            await _majorRepo.SaveAsync();
            return true;
        }
    }
}
