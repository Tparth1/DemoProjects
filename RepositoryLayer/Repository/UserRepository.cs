using DomainLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T : BaseEntity
    {
        private readonly ConnectionString _context;

        private readonly DbSet<T> _users;

        public UserRepository(ConnectionString context)
        {
            _context = context;
            _users = _context.Set<T>();
        }

        public async Task<bool> AddUser(T entity)
        {
            await _context.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(T entity)
        {
            try
            {
                _users.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<T> Find(Expression<Func<T, bool>> match)
        {
            return await _users.FirstOrDefaultAsync(match);
        }

        public async Task<IEnumerable<T>> GetAllUser()
        {
           return await _users.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _users.FirstOrDefaultAsync(x=> x.ID ==id);      
        }

        public async Task<T> GetLast()
        {
            return await _users.OrderByDescending(x=> x.ID).FirstOrDefaultAsync();

        }

        public async Task<bool> UpdateUser(T entity)
        {
            try
            {
                _users.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
