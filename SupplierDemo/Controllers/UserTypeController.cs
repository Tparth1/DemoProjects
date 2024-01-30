using DomainLayer.Model;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.CustomService.UserCustomService;

namespace SupplierDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var userType = await _userTypeService.GetAllUserTypesAsync();
            return Ok(userType);
        }
        [HttpGet("GetUserTypeById/{id}")]
        public async Task<IActionResult> GetUserTypeById(Guid id)
        {
            var userId = _userTypeService.GetUserTypeByIdAsync(id);
            if(userId == null)
            {
                return NotFound();
            }
            return Ok(userId);
        }
        [HttpPost("AddUserType")]
        public async Task<IActionResult> AddUserType([FromBody] UserTypeInsertView userTypeInsertView)
        {
            var userTypeId = await _userTypeService.AddUserTypeAsync(userTypeInsertView);
            return Ok(userTypeId);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserTypeUpdateView userTypeView)
        {
            if(id != userTypeView.UserTypeId)
            {
                return BadRequest();
            }
            await _userTypeService.UpdateUserTypeAsync(userTypeView);
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] UserTypeViewModal userTypeView)
        {
            var result = await _userTypeService.DeleteUserTypeAsync(userTypeView);
            if(result)
            {
                return NoContent();
            }
            return Ok();
        }

    }
}
