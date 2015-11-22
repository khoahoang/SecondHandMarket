using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TraoDoiDoCu.Models.Account
{
    public class DisplayUserInfoViewModel
    {
        [Required]
        [Display(Name = "Tên tài khoản:")]
        public string UserName{get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu:")]
        public string Password{get; set;}
        
        [Required]
        [Display(Name = "Họ:")]
        public string FirstName{get; set;}

        [Required]
        [Display(Name = "Tên:")]
        public string LastName{get; set;}
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email{get; set;}
        [Required]
        [Display(Name = "Điện thoại:")]
        public string PhoneNumber { get; set; }
    }
}