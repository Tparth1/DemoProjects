using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Category:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public virtual List<Item> Items { get; set;}
    }
}
