using DomainLayer.Model;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.CategoryCustomService
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(CategoryInsertView categoryInsertView);
        Task<CategoryViewModal> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<CategoryViewModal>> GetAllCategorysAsync();
        Task<bool> DeleteCategoryAsync(CategoryViewModal categoryViewModal);

        Task<bool> UpdateCategoryAsync(CategoryUpdateView categoryUpdateView);
    }
}
