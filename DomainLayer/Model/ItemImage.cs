using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class ItemImage:BaseEntity
    {
        public Guid ItemImageId { get; set; }
        [Required]

        public string ItemImages { get; set; }
        public virtual Item Item { get; set; }
    }
}
