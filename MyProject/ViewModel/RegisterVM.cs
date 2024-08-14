using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Không được để trống!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        [Compare("Password", ErrorMessage = "Mật khẩu phải trùng khớp!")]
        public string ConfirmPass { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        [EmailAddress(ErrorMessage = "Ký tự không hợp lệ!")]
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}