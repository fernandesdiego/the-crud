using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCRUD.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "50 chars maximum")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="50 chars maximum")]
        public string Description { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "40 chars maximum")]
        public string Category { get; set; }
        [Required]
        public int Amount { get; set; }
        [Display(Name = "Last Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

    }
}
