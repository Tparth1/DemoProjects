using DomainLayer;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.GeneralService
{
    public class UserGeneralService<T> : IUserGeneralServices<T> where T : BaseEntity
    {
        private readonly IUserRepository<T> _repository;

        public UserGeneralService(IUserRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddUserAsync(T entity)
        {
            return await _repository.AddUser(entity);
        }

        public async Task<IEnumerable<T>> GetAllUserAsync()
        {
           return await _repository.GetAllUser();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _repository.GetById(id);
        }

        public Task<T> GetLastAsync()
        {
            return _repository.GetLast();
        }

        public async Task<bool> UpdateUserAsync(T entity)
        {
            return await _repository.UpdateUser(entity);
        }

        public async Task<bool> DeleteUserAsync(T entity)
        {
            return await _repository.DeleteUser(entity);
        }

        public async Task<T> Find(Expression<Func<T, bool>> match)
        {
            return await _repository.Find(match);
        }
    }
}
