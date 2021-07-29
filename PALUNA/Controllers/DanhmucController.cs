using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PALUNA.Models;
using PALUNA.Models.ViewModel;

namespace PALUNA.Controllers
{
    public class DanhmucController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // GET: Danhmuc
        public ActionResult DanhmucPartial(int? typeId)
        {
            var listProducts = db.Sanphams
                .Where(n => n.LoaiSp != null && typeId == null || n.LoaiSp == typeId)
                .OrderBy(m => m.Tensp).ToList()
                .GroupBy(m => m.Hangsanxuat).OrderBy(m => m.Key.Tenhang);
            return PartialView(listProducts);
        }
    }
}