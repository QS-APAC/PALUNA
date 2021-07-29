using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PALUNA.Models;
using PALUNA.Models.ViewModel;

namespace PALUNA.Controllers
{
    public class GioHangController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // GET: GioHang

        //Lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            Sanpham sp = db.Sanphams.SingleOrDefault(n => n.Masp == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanpham = lstGioHang.Find(n => n.iMasp == iMasp);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasp);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            Sanpham sp = db.Sanphams.SingleOrDefault(n => n.Masp == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //Kiểm tra masp
            Sanpham sp = db.Sanphams.SingleOrDefault(n => n.Masp == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMasp == iMaSP);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        #region Đặt hàng
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            Donhang ddh = new Donhang();
            Nguoidung kh = (Nguoidung)Session["use"];
            List<GioHang> gh = LayGioHang();
            ddh.MaNguoidung = kh.MaNguoiDung;
            ddh.Ngaydat = DateTime.Now;
            db.Donhangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                Chitietdonhang ctDH = new Chitietdonhang();
                ctDH.Madon = ddh.Madon;
                ctDH.Masp = item.iMasp;
                ctDH.Soluong = item.iSoLuong;
                ctDH.Dongia = (decimal)item.dDonGia;
                db.Chitietdonhangs.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        //[HttpPost]
        //public async Task<string> CartPayment(int userId)
        //{
        //    var result = "";
        //    try
        //    {
        //        var user = await db.Nguoidungs.FindAsync(userId);
        //        result = user != null ? JsonConvert.SerializeObject(user) : "'";
        //    }
        //    catch (Exception e)
        //    {
        //        return "";
        //    }
        //    return result;

        //}
        [HttpPost]
        public ActionResult CartPayment(CartPaymentViewModel cartModel)
        {

            if (Session["use"] != null && Session["use"] is Nguoidung user)
            {
                if (!ModelState.IsValid)
                {
                    var result = ModelState.Where(m => m.Value.Errors.Any()).Select(m =>
                        new { key = m.Key, errs = m.Value.Errors.Select(n => n.ErrorMessage) });
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(result));
                }
                else
                {
                    return CreateCart(cartModel)
                        ? new HttpStatusCodeResult(HttpStatusCode.OK, "Xử lý dữ liệu thành công")
                        : new HttpStatusCodeResult(HttpStatusCode.InternalServerError,"Xử lý dữ liệu thất bại");
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Hết phiên đăng nhập");
            }

        }

        private bool CreateCart(CartPaymentViewModel cartModel)
        {
            try
            {
                Donhang ddh = new Donhang();
                Nguoidung kh = (Nguoidung)Session["use"];
                List<GioHang> gh = LayGioHang();
                ddh.MaNguoidung = cartModel.CartUser?.MaNguoiDung;
                ddh.Ngaydat = DateTime.Now;
                ddh.DeliveryInfo = cartModel.CartUser == null ? null : JsonConvert.SerializeObject(cartModel.CartUser);
                ddh.Madon = DateTime.Now.ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
                db.Donhangs.Add(ddh);

                db.SaveChanges();
                //Thêm chi tiết đơn hàng
                foreach (var item in cartModel.ListItem)
                {
                    Chitietdonhang ctDH = new Chitietdonhang();
                    ctDH.Madon = ddh.Madon;
                    ctDH.Masp = item.iMasp;
                    ctDH.Soluong = item.iSoLuong;
                    ctDH.Dongia = (decimal)item.dDonGia;
                    db.Chitietdonhangs.Add(ctDH);
                }
                db.SaveChanges();
                if (Session["GioHang"] != null && Session["GioHang"] is List<GioHang> listCarts && cartModel.ListItem != null)
                {
                    Session["GioHang"] = listCarts.Where(m => cartModel.ListItem.All(n => n.iMasp != m.iMasp)).ToList();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}