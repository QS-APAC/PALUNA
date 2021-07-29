using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PALUNA.Models;

namespace PALUNA.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/LoaiSanPhams
        public ActionResult Index()
        {
            var u = Session["use"] as PALUNA.Models.Nguoidung;
            if (Session["use"] == null || u.IDQuyen != 2)
            {
                return RedirectToAction("Index", "../Home");
            }
            return View(db.LoaiSanPhams.ToList());
        }

        // GET: Admin/LoaiSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham LoaiSanPham = db.LoaiSanPhams.Find(id);
            if (LoaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(LoaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoaiSanPham LoaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSanPhams.Add(LoaiSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(LoaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham LoaiSanPham = db.LoaiSanPhams.Find(id);
            if (LoaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(LoaiSanPham);
        }

        // POST: Admin/LoaiSanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoaiSanPham LoaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(LoaiSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(LoaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham LoaiSanPham = db.LoaiSanPhams.Find(id);
            if (LoaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(LoaiSanPham);
        }

        // POST: Admin/LoaiSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSanPham LoaiSanPham = db.LoaiSanPhams.Find(id);
            db.LoaiSanPhams.Remove(LoaiSanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
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
