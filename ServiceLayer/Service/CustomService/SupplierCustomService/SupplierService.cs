using DomainLayer.Model;
using DomainLayer.ViewModel;
using RepositoryLayer.IRepository;
using ServiceLayer.Service.CustomService.UserCustomService;
using ServiceLayer.Service.GeneralService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.SupplierCustomService
{
    public class SupplierService : ISupplierService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IUserGeneralServices<UserType> _userGeneralServices;

        public SupplierService(IUserRepository<User> userRepository, IUserGeneralServices<UserType> userGeneralServices)
        {
            _userRepository = userRepository;
            _userGeneralServices = userGeneralServices;
        }

        public async Task<bool> AddSupplierAsync(UserInsertView supplierItem, string photo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplierItem.UserName) || string.IsNullOrWhiteSpace(supplierItem.UserEmail))
                {
                    return false;
                }
                var isSupplier = await _userGeneralServices.Find(x => x.UserTypeName == "Supplier");
                var newUser = new User()
                {
                    UserId = supplierItem.userId,
                    UserName = supplierItem.UserName,
                    Address = supplierItem.Address,
                    PhoneNumber = supplierItem.PhoneNumber,
                    UserEmail = supplierItem.UserEmail,
                    UserPassword = supplierItem.UserPassword,
                    ProfilePic = photo,
                    UserTypeId = isSupplier.ID,
                    IsActive = supplierItem.IsActive,

                };

                return await _userRepository.AddUser(newUser);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id);

                if (user == null)
                {
                    return false;
                }

                return await _userRepository.DeleteUser(user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserViewModal>> GetAllSuppliersAsync()
        {
            var users = await _userRepository.GetAllUser();

            var userViewModals = users.Select(user => new UserViewModal
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
            });
            return userViewModals;

        }

        public async Task<UserViewModal> GetSupplierByIdAsync(Guid id)
        {
            var user = await _userRepository.GetById(id);

            var userViewModal = new UserViewModal
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                
            };

            return userViewModal;
        }

        public async Task<bool> UpdateSupplierAsync(UserUpdateView userUpdateView, string photo)
        {
            try
            {
                var user = await _userRepository.GetById(userUpdateView.Id);

                if (user == null)
                {
                    return false;
                }

                user.UserName = userUpdateView.UserName;
                user.UserEmail = userUpdateView.UserEmail;
                user.PhoneNumber = userUpdateView.PhoneNumber;
                user.Address = userUpdateView.Address;
                user.UserPassword = userUpdateView.UserPassword;

                return await _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
