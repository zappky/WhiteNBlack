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
            return View();
        }
    }
}