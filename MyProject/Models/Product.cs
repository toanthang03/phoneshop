using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Price { get; set; }
        public string ProductImage { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}