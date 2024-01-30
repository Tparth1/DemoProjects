using DomainLayer.Model;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.UserCustomService
{
    public interface IUserTypeService
    {
        Task<bool> AddUserTypeAsync(UserTypeInsertView userType);
        Task<UserTypeViewModal> GetUserTypeByIdAsync(Guid id);
        Task<IEnumerable<UserTypeViewModal>> GetAllUserTypesAsync();
        Task<bool> DeleteUserTypeAsync(UserTypeViewModal userTypeView);

        Task<bool> UpdateUserTypeAsync(UserTypeUpdateView userType);
        Task<UserType> Find(Expression<Func<UserType,bool>>match);

    }
}
