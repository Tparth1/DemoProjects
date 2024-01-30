using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class User:BaseEntity
    {
        public string UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string UserEmail { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must meet complexity requirements.")]
        public string UserPassword { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10,15}$")]
        public long PhoneNumber {  get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [ForeignKey("UserTypeId")]
        public Guid UserTypeId { get; set; }
        public string ProfilePic { get; set; }
        public virtual UserType UserType { get; set;}
        public  List<SupplierItem> SupplierItems { get; set; }
        public List<CustomerItem> CustomerItems { get; set; }
    }
}
