using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class UserTypeViewModal
    {
        public Guid UserTypeId { get; set; }
     
        public string UserTypeName { get; set; }

    }
    public class UserTypeInsertView
    {
        [Required, MaxLength(100)]
        public string UserTypeName { get; set; }
    }

    public class UserTypeUpdateView: UserTypeInsertView
    {
        [Required]
        public Guid UserTypeId { get; set; }
    }

}
