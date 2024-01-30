using DomainLayer.Model;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.CustomerCustomService
{
    public interface ICustomerService
    {
        Task<bool> AddCustomerItemAsync(UserInsertView customerItem,string photo);
        Task<UserViewModal> GetCustomerItemByIdAsync(Guid id);
        Task<IEnumerable<UserViewModal>> GetAllCustomerItemsAsync();
        Task<bool> DeleteCustomerItemAsync(Guid id);
        Task<bool> UpdateSupplierAsync(UserUpdateView userUpdateView, string photo);
    }
}
