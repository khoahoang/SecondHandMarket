using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraoDoiDoCu.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Tên tài khoản ít nhất 6 kí tự và nhiều nhất 25 kí tự.")]
        [Display(Name = "Tên tài khoản:")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 kí tự và nhiều nhất 50 kí tự.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không trùng nhau.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}