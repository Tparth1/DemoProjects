using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class SupplierItem: BaseEntity
    {
        public Guid UserId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public Guid ItemId {  get; set; }
        public virtual Item Item { get; set; }


    }

}
