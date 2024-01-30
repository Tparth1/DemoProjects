using DomainLayer.Model;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Data;
using ServiceLayer.Service.CustomService.CustomerCustomService;
using ServiceLayer.Service.CustomService.SupplierCustomService;
using ServiceLayer.Service.CustomService.UserCustomService;
using ServiceLayer.Service.GeneralService;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SupplierDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<LoginController> _logger;
        private readonly ISupplierService _supplierService;
        private readonly ICustomerService _customerService;
        private readonly IUserGeneralServices<UserType> _userGeneralServices;


        public LoginController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IUserGeneralServices<UserType> userGeneralServices, ILogger<LoginController> logger, ISupplierService supplierService, ICustomerService customerService, IUserTypeService userTypeService)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _supplierService = supplierService;
            _customerService = customerService;
            _userGeneralServices = userGeneralServices;
     

        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginModel loginModel)
        {
            if (!string.IsNullOrEmpty(loginModel.UserName) && !string.IsNullOrEmpty(loginModel.Password))
            {
                var token = GenerateJwtToken(loginModel.UserName);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [HttpPost("RegisterSupplier")]
        public async Task<IActionResult> RegisterSupplier([FromForm] UserInsertView userInsertView)
        {
            _logger.LogInformation("==========================Registration for supplier with validation===================================");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userType = await _userGeneralServices.Find(x => x.UserTypeName == "Supplier");
            if(userType == null)
            {
                ModelState.AddModelError("UserTypeId", "Invalid user type for supplier registration.");
                return BadRequest(ModelState);
            }

            string profilePicUrl = await UploadImage(userInsertView.ProfilePic, userInsertView.userId, DateTime.Now);
            if (string.IsNullOrEmpty(profilePicUrl))
            {
                ModelState.AddModelError("ProfilePic", "Error uploading profile picture.");
                return BadRequest(ModelState);
            }
            var registrationSuccess = await _supplierService.AddSupplierAsync(userInsertView, profilePicUrl);
            if (!registrationSuccess)
            {
                ModelState.AddModelError("", "Error registering supplier. Please try again.");
                return BadRequest(ModelState);
            }

            return Ok("Register of Supplier Succesfull");
        }

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromForm] UserInsertView userInsertView)
        {
            _logger.LogInformation("==========================Registration for Customer with validation===================================");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userType = await _userGeneralServices.Find(x => x.UserTypeName == "Customer");
            if (userType == null)
            {
                ModelState.AddModelError("UserTypeId", "Invalid user type for Customer registration.");
                return BadRequest(ModelState);
            }
            if (userInsertView.ProfilePic == null || userInsertView.ProfilePic.Length == 0)
            {
                ModelState.AddModelError("ProfilePic", "Profile picture is required.");
                return BadRequest(ModelState);
            }
            string profilePicUrl = await UploadImage(userInsertView.ProfilePic, userInsertView.userId, DateTime.Now);
            if (string.IsNullOrEmpty(profilePicUrl))
            {
                ModelState.AddModelError("ProfilePic", "Error uploading profile picture.");
                return BadRequest(ModelState);
            }
            var registrationSuccess = await _customerService.AddCustomerItemAsync(userInsertView, profilePicUrl);
            if (!registrationSuccess)
            {
                ModelState.AddModelError("", "Error registering Customer. Please try again.");
                return BadRequest(ModelState);
            }

            return Ok("Register of Customer Succesfull");
            //var token = GenerateJwtToken(userInsertView.UserName);
            //return Ok(new { Token = token });

        }

        private async Task<string> UploadImage(IFormFile file,string id,DateTime date)
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

        private string GenerateJwtToken(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(120),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
