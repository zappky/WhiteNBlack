using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmokeFreeApplication.Models;


namespace SmokeFreeApplication.Controllers
{
    public class DoctorController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor
        public ActionResult DrProfile()
        {
            return View();
        }

        public FileContentResult retrieveUserPic(string username)
        {
            byte[] imgByteArray = smokeFreeDB.GeneralUser.Find(username).profilePicture;
            if (imgByteArray != null)
            {
                return new FileContentResult(imgByteArray, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
    }
}