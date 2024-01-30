using DomainLayer.Model;
using DomainLayer.ViewModel;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.CustomService.CategoryCustomService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUserRepository<Category> _userRepository;

        public CategoryService(IUserRepository<Category> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddCategoryAsync(CategoryInsertView categoryInsertView)
        {
            var create = new Category
            {
                CategoryName = categoryInsertView.CategoryName
            };
            return await _userRepository.AddUser(create);
        }

        public async Task<bool> DeleteCategoryAsync(CategoryViewModal categoryView)
        {
            var category = await _userRepository.GetById(categoryView.CategoryId);
            if (category == null)
            {
                return false;
            }
            return await _userRepository.DeleteUser(category);
        }

        public async Task<IEnumerable<CategoryViewModal>> GetAllCategorysAsync()
        {
            var category = await _userRepository.GetAllUser();
            return category.Select(x => new CategoryViewModal
            {
                CategoryId = x.ID,
                CategoryName = x.CategoryName
            });
        }

        public async Task<CategoryViewModal> GetCategoryByIdAsync(Guid id)
        {
            var categoryId = await _userRepository.GetById(id);
            if (categoryId == null)
            {
                return null;
            }
            return new CategoryViewModal
            { CategoryId = categoryId.ID, CategoryName = categoryId.CategoryName };
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateView categoryUpdateView)
        {
            var categoryId = await _userRepository.GetById(categoryUpdateView.CategoryId);
            if (categoryId == null)
            {
                return false;
            }
            categoryId.CategoryName = categoryUpdateView.CategoryName;
            return await _userRepository.UpdateUser(categoryId);
        }
    }
}
