using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PALUNA.Utilties;

namespace PALUNA.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(int? typeId)
        {
            ViewBag.TypeId = typeId;
            return View();

        }
        public ActionResult Search()
        {

            return View();

        }
        public ActionResult Type(Enum type)
        {

            return View("Index");

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();

        }
    }
}