using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PALUNA.Models;
using PALUNA.Models.Dto;
using PALUNA.Models.ViewModel;

namespace PALUNA.Controllers
{
    public class SanphamController : Controller
    {
        Qlbanhang db = new Qlbanhang();

        // GET: Sanpham

        public ActionResult ProductFilterPartial(int? model, int? typeId)
        {
           var data = db.Sanphams.Where(n => (model == null || n.Mahang == model) && (typeId == null || n.LoaiSp == typeId))
                .Select(m=> new ProductViewModel(m));

           var result = data.OrderBy(m => m.Tenhang).GroupBy(m=>m.Mahang);
            return PartialView(data.ToList());
        }
        [HttpPost]
        public string Filter(int? modelId, int? skip, int? take, int? typeId)
        {
            IQueryable<Sanpham> data = db.Sanphams.Where(m => m.LoaiSp != null && typeId == null || m.LoaiSp == typeId);
            if (modelId != null)
            {
                data = data.Where(n => n.Mahang == modelId);
            }

            data = data.OrderBy(m => m.Tensp).Skip(skip ?? 0);
            if (take.HasValue)
            {
                data = data.Take(take.Value);
            }

            var result = data.ToList().Select(m => new ProductModel(m));
            return JsonConvert.SerializeObject(result);
        }
        public ActionResult dtiphonepartial()
        {
            var ip = db.Sanphams.Where(n => n.Mahang == 2).Take(4).ToList();
            return PartialView(ip);
        }
        public ActionResult dtsamsungpartial()
        {
            var ss = db.Sanphams.Where(n => n.Mahang == 1).Take(4).ToList();
            return PartialView(ss);
        }
        public ActionResult dtxiaomipartial()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 3).Take(4).ToList();
            return PartialView(mi);
        }
        //public ActionResult dttheohang()
        //{
        //    var mi = db.Sanphams.Where(n => n.Mahang == 5).Take(4).ToList();
        //    return PartialView(mi);
        //}
        public List<string> ListName(string keywork)
        {
            return db.Sanphams.Where(x => x.Tensp.Contains(keywork)).Select(x => x.Tensp).ToList();
        }


        public ActionResult xemchitiet(int Masp = 0)
        {
            var chitiet = db.Sanphams.SingleOrDefault(n => n.Masp == Masp);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }

    }

}