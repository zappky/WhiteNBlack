using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SmokeFreeApplication.Models;

namespace SmokeFreeApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie aCookie = new HttpCookie("popup", "true");
            Response.Cookies.Add(aCookie);

            //private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
            SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
            var msg = smokeFreeDB.BroadCastMessage.First();

            return View(msg);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

            return View();
        }


    }
}