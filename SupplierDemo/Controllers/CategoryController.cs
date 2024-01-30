using DomainLayer.Model;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.CustomService.CategoryCustomService;

namespace SupplierDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var user = await _categoryService.GetAllCategorysAsync();
            return Ok(user);
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryInsertView categoryInsertView)
        {
            await _categoryService.AddCategoryAsync(categoryInsertView);
            return Ok();

        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var userId = _categoryService.GetCategoryByIdAsync(id);
            if (userId == null)
            {
                return NotFound();
            }
            return Ok(userId);

        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateView categoryUpdate)
        {

            await _categoryService.UpdateCategoryAsync(categoryUpdate);
            return Ok();
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(CategoryViewModal categoryView)
        {
            await _categoryService.DeleteCategoryAsync(categoryView);
            return Ok();
        }

    }
}
