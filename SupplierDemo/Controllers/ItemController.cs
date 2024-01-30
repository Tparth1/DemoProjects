using DomainLayer.Model;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.CustomService.CustomerCustomService;
using ServiceLayer.Service.CustomService.ItemCustomService;
using ServiceLayer.Service.CustomService.SupplierCustomService;
using ServiceLayer.Service.GeneralService;

namespace SupplierDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetItemsDetails")]
        public async Task<IActionResult> GetItemsDetails()
        {
            var item = await _itemService.GetAllItemsAsync();
            return Ok(item);
        }
        [HttpGet("GetItemsById/{id}")]
        public async Task<IActionResult> GetItemsById(Guid id)
        {
            var itemId = _itemService.GetItemByIdAsync(id);
            if (itemId == null)
            {
                return NotFound();
            }
            return Ok(itemId);
        }
        [HttpPost("AddItems")]
        public async Task<IActionResult> AddItems([FromForm] ItemInsertViewModal itemInsertView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string profilePicUrl = await UploadImage(itemInsertView.ItemImages, itemInsertView.ItemName, DateTime.Now);
            if (string.IsNullOrEmpty(profilePicUrl))
            {
                ModelState.AddModelError("ProfilePic", "Error uploading profile picture.");
                return BadRequest(ModelState);
            }
            await _itemService.AddItemAsync(itemInsertView, profilePicUrl);
            return Ok();
        }
        [HttpDelete("DeleteItems")]
        public async Task<IActionResult> DeleteItems(ItemViewModal itemViewModal)
        {
            await _itemService.DeleteItemAsync(itemViewModal);
            return Ok();
        }
        [HttpPut("UpdateItems/{id}")]
        public async Task<IActionResult> UpdateItems(ItemUpdateViewModal itemUpdateView)
        {

            await _itemService.UpdateItemAsync(itemUpdateView);
            return Ok();
        }
        private async Task<string> UploadImage(IFormFile file, string id, DateTime date)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid file type. Allowed types are: jpg, jpeg, png, gif");
            }

            var fileName = $"{id}_{date:yyyyMMddHHmmssfff}{fileExtension}";

            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "images");

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var filePath = Path.Combine(uploadDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = Path.Combine("uploads", fileName);

            return relativePath;

        }

    }
}
