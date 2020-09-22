using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokeFreeApplication.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Articles()
        {
            ViewBag.Message = "View articles written by certified doctors";
            return View();
        }
    }
}