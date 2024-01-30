using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class UserType:BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string UserTypeName { get; set; }

        public virtual List<User> Users { get; set; }

    }
}
