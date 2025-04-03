using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repo;

        public UserService(IGenericRepository<User> repo)
        {
            _repo = repo;
        }

        public async Task<User?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<User> CreateAsync(User entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<User?> UpdateAsync(Guid id, User entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

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
