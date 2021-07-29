using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PALUNA.CustomAttribute;

namespace PALUNA.Models.ViewModel
{
    public class CartPaymentViewModel
    {
        public UserCartViewModel CartUser { get; set; }
        public IEnumerable<CartItemDetailViewModel> ListItem { get; set; }

    }
    public class CartItemDetailViewModel
    {
        public int iMasp { get; set; }
        public int iSoLuong { get; set; }
        public int dDonGia { get; set; }
    }
    public class UserCartViewModel
    {
        [Required(ErrorMessage = "required")]
        public string Hoten { get; set; }
        [Required(ErrorMessage = "required")]
        public int MaNguoiDung { get; set; }
        [Required(ErrorMessage = "required")]
        [EmailValidation("wrongformat")]
        public string Email { get; set; }
        [Required(ErrorMessage = "required")]
        [NumberPhoneValidation("wrongformat")]
        public string Dienthoai { get; set; }
        [Required(ErrorMessage = "required")]
        public string Diachi { get; set; }
    }

}