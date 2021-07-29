using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PALUNA.CustomAttribute;

namespace PALUNA.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Bạn không được để trống trường này!")]
        [EmailValidation("Bạn đã nhập chưa đúng định dạng Email!")]
        [Display(Name = "Email")]

        public string userMail { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Bạn không được để trống trường này!")]

        public string password { get; set; }
    }
}