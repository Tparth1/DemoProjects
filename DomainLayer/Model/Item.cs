using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Item: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; }
        [MaxLength(50)]
        public string ItemCode { get; set; }
        [MaxLength(500)]
        public string ItemDescription { get; set; }
        [Required]
        [MaxLength(50)]
        public string ItemPrice { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set;}
        public virtual CustomerItem CustomerItem { get; set; }
        public virtual SupplierItem SupplierItem { get; set; }
        public virtual List<ItemImage> ItemImages { get; set; }

    }
}
