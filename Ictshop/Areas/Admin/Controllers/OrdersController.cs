using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PALUNA.Areas.Admin.Models.ViewModel;
using PALUNA.Models;

namespace PALUNA.Areas.Admin.Controllers
{
    public class ordersController : Controller
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/Hedieuhanhs
        public ActionResult Index()
        {
            var u = Session["use"] as PALUNA.Models.Nguoidung;
            if (Session["use"] == null || u.IDQuyen != 2)
            {
                return RedirectToAction("Index", "../Home");
            }
            return View(db.Donhangs.OrderByDescending(m => m.Ngaydat).ToList().Select(m => new OrderViewModel(m)));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
