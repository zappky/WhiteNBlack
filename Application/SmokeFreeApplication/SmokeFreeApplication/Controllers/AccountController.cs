using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokeFreeApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult SignUpMember()
        {
            ViewBag.Message = "Sign up here";
            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Sign in here";
            return View();
        }


    }
}