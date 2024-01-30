using DomainLayer.Model;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.SupplierCustomService
{
    public interface ISupplierService
    {
        Task<bool> AddSupplierAsync(UserInsertView supplierItem,string photo);
        Task<UserViewModal> GetSupplierByIdAsync(Guid id);
        Task<IEnumerable<UserViewModal>> GetAllSuppliersAsync();
        Task<bool> DeleteSupplierAsync(Guid id);
        Task<bool> UpdateSupplierAsync(UserUpdateView userUpdateView, string photo);
    }
}
