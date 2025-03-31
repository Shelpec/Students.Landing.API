using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _repo;

        public StudentService(IGenericRepository<Student> repo)
        {
            _repo = repo;
        }

        public async Task<Student?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Student>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Student> CreateAsync(Student entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<Student?> UpdateAsync(Guid id, Student entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.FirstName = entity.FirstName;
            existing.LastName = entity.LastName;

            existing.MajorId = entity.MajorId;
            existing.PhotoUrl = entity.PhotoUrl;
            existing.KeycloakUserId = entity.KeycloakUserId;

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

        //Реализация нового метода для поиска по KeycloakUserId
        public async Task<Student?> GetStudentByKeycloakIdAsync(string keycloakUserId)
        {
            // Здесь можно оптимизировать, если в репозитории есть метод, позволяющий
            // фильтровать напрямую в БД. К примеру: _repo.Query().FirstOrDefaultAsync(...).
            // Пока для наглядности используем GetAllAsync():
            var allStudents = await _repo.GetAllAsync();
            return allStudents.FirstOrDefault(s => s.KeycloakUserId == keycloakUserId);
        }
    }
}
