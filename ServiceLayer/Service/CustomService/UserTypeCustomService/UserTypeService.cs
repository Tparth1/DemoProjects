using DomainLayer.Model;
using DomainLayer.ViewModel;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.UserCustomService
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserRepository<UserType> _userRepository;

        public UserTypeService(IUserRepository<UserType> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserTypeAsync(UserTypeInsertView userTypeInsert)
        {
            var userType = new UserType
            {
                UserTypeName = userTypeInsert.UserTypeName,
            };

            return await _userRepository.AddUser(userType);
        }

        public async Task<bool> DeleteUserTypeAsync(UserTypeViewModal userTypeView)
        {
            var userType = await _userRepository.GetById(userTypeView.UserTypeId);
            if (userType == null)
            {
                return false;
            }
            return await _userRepository.DeleteUser(userType);

        }

        public async Task<IEnumerable<UserTypeViewModal>> GetAllUserTypesAsync()
        {
            var userType = await _userRepository.GetAllUser();
            return userType.Select(x => new UserTypeViewModal
            {
                UserTypeId = x.ID, UserTypeName = x.UserTypeName
            });
        }

        public async Task<UserTypeViewModal> GetUserTypeByIdAsync(Guid id)
        {
           var userType = await _userRepository.GetById(id);
            if(userType == null)
            {
                return null;
            }
            return new UserTypeViewModal
            { UserTypeId = userType.ID, UserTypeName = userType.UserTypeName };

        }

        public async Task<bool> UpdateUserTypeAsync(UserTypeUpdateView userTypeUpdate)
        {
            var userId = await _userRepository.GetById(userTypeUpdate.UserTypeId);
            if (userId == null)
            {
                return false;
            }
            userId.UserTypeName = userTypeUpdate.UserTypeName;
            return  await _userRepository.UpdateUser(userId);
        }

        public Task<UserType> Find(Expression<Func<UserType, bool>> match)
        {
            return _userRepository.Find(match);
        }
    }
}
