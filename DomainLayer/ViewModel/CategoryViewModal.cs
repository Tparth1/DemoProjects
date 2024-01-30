using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class CategoryViewModal
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryInsertView 
    {
        [Required, MaxLength(100)]
        public string CategoryName { get; set; }
    }
    public class CategoryUpdateView: CategoryInsertView
    {
        [Required]
        public Guid CategoryId { get; set; }
    }
}
