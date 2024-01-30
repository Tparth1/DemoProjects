using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [DefaultValue(true)]
        [Display(Name = "IsActive")]
        public bool? IsActive { get; set; }
                
    }
}
