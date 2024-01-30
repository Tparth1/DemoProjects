using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.GeneralService
{
    public interface IUserGeneralServices<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllUserAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetLastAsync();
        Task<bool> AddUserAsync(T entity);
        Task<bool> UpdateUserAsync(T entity);
        Task<bool> DeleteUserAsync(T entity);
        Task<T> Find(Expression<Func<T, bool>> match);
    }
}
