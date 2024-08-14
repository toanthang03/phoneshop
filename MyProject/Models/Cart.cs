using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public string userName { get; set; }

        public virtual Product Product { get; set; }
    }
}
