using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmokeFreeApplication.Models;


namespace SmokeFreeApplication.Controllers
{
    public class ArticleController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();

        // GET: Article
        public ActionResult Articles()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }

            return View(smokeFreeDB.Article.ToList());
        }

        // Borrow code from ray!
    }

    }
}