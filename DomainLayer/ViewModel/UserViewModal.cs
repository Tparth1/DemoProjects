using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class UserViewModal
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }
        public List<UserTypeViewModal> UserTypeViewModal { get; set; } = new List<UserTypeViewModal>();

    }
    public class UserInsertView
    {
        [Required]
        public string userId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required, EmailAddress, MaxLength(50)]
        public string UserEmail { get; set; }
        [Required, MaxLength(100)]
        public string UserPassword { get; set; }
        [Required, RegularExpression(@"^[0-9]{10,15}$")]
        public long PhoneNumber { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [Required]
        public IFormFile ProfilePic { get; set; }
        public bool? IsActive { get; set; }

    }
    public class UserUpdateView:UserInsertView
    {
        [Required]
        public Guid Id { get; set;}
    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
    }

}
