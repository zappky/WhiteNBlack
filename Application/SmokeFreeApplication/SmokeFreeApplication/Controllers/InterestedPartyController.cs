using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokeFreeApplication.Controllers
{
    public class InterestedPartyController : Controller
    {
        // GET: InterestedParty
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemberProfile()
        {
            return View();
        }
        public ActionResult CreateStory()
        {
            return View();
        }
        public ActionResult QuitSmokingTracker()
        {
            return View();
        }

        public ActionResult ClickLogOut()
        {

            Session.Clear();
            return View("../Home/Index");

        }
    }
}