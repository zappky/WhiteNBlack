using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokeFreeApplication.Controllers
{
    public class StoryController : Controller
    {
        // GET: Story
        public ActionResult Stories()
        {
            if(Session["username"] != null)
            {

            }
            else
            {
                TempData["message"] = "Please sign in to see stories!";
            }


            return View();
        }
    }
}