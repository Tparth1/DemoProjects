using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IUserRepository<T> where T : BaseEntity
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAllUser();
        Task<T> GetLast();
        Task<bool> AddUser(T entity);
        Task<bool> UpdateUser(T entity);
        Task<bool> DeleteUser(T entity);
        Task<T>Find(Expression<Func<T, bool>> match);
    }
}
