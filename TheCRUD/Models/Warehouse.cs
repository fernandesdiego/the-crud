using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheCRUD.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:##-####-####}", ApplyFormatInEditMode = true)]
        public string Phone { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
