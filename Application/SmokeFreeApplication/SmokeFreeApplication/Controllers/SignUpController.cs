using SmokeFreeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SmokeFreeApplication.Controllers
{
    public class SignUpController : Controller
    {
        // GET: User/Signup
        public ActionResult SignUp()
        {
            var user = new User() { };

            return View(user);
        }
    }
}