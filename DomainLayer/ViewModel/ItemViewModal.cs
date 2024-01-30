using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class ItemViewModal
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ItemPrice { get; set; }
        public Guid CategoryId { get; set; }
        
        public List<CategoryInsertView> Categorys { get; set; } = new List<CategoryInsertView>();
        public List<UserView> Users { get; set; } = new List<UserView>(); 
        public List<ItemImageView> Images { get; set; } = new List<ItemImageView>();
    }
    public class ItemImageView
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string ItemImageCollection { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UserView
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }

    }

    public class ItemInsertViewModal
    {
        [Required, MaxLength(100)]
        public string ItemName { get; set; }
        [Required, MaxLength(100)]
        public string ItemCode { get; set; }
        [Required, MaxLength(1000)]
        public string ItemDescription { get; set; }
        [Required, MaxLength(1000)]
        public string ItemPrice { get; set; }
        [Required]
        public Guid UserId {  get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public IFormFile ItemImages { get; set; }
        public bool? IsActive { get; set; }
    }
    public class ItemUpdateViewModal : ItemInsertViewModal
    {
        public Guid ItemId { get; set; }
    }
}
