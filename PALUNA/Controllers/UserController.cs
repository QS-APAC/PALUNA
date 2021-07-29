using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PALUNA.Models;
namespace PALUNA.Controllers
{
    public class UserController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // ĐĂNG KÝ
        public ActionResult Dangky()
        {
            return View();
        }

        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        public ActionResult Dangky(Nguoidung nguoidung)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var existUser = db.Nguoidungs.FirstOrDefault(m => m.Email.Equals(nguoidung.Email, StringComparison.OrdinalIgnoreCase));
                if (existUser != null)
                {
                    ModelState.AddModelError("Email", $"Email {nguoidung.Email} đã được sử dụng.");
                    return View();
                }
                // Thêm người dùng  mới
                db.Nguoidungs.Add(nguoidung);
                // Lưu lại vào cơ sở dữ liệu
                db.SaveChanges();
                var islogin = db.Nguoidungs.SingleOrDefault(x => x.Email.Equals(nguoidung.Email));
                if (islogin != null)
                {
                    {
                        Session["use"] = islogin;
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Dangnhap");
                }
                return View("Dangky");

            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Dangnhap()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Dangnhap(LoginModel userlog)
        {
            string userMail = userlog.userMail;
            string password = userlog.password;
            if (!ModelState.IsValid)
            {
                return View();
            }
            var islogin = db.Nguoidungs.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));

            if (islogin != null)
            {
                if (userMail == "Admin@gmail.com" || userMail == "admin@gmail.com")
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/Home");
                }
                else
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View();
            }

        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }


    }
}