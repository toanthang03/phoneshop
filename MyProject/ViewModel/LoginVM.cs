using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Không được để trống!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Password { get; set; }
    }
}