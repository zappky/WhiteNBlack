using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmokeFreeApplication.Models;
using PagedList;


namespace SmokeFreeApplication.Controllers
{
    public class DoctorController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        public ActionResult Index()
        {
            return View();
        }

        //viewUsername = username of the profile you want to view
        public ActionResult DrProfile(string viewUsername, int? page)
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            List<Article> displayList = new List<Article>();

            //If null, redirect user back to sign in page
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }
            else
            {
                //Get username of current user logged in
                string username = Session["username"].ToString();

                //Get info of profile that is being viewed
                var generaldata = smokeFreeDB.GeneralUser.Where(x => x.userName.Equals(viewUsername));
                var docdata = smokeFreeDB.Doctor.Where(x => x.userName.Equals(viewUsername));

                //Get doctor info & list of articles written
                displayList = smokeFreeDB.Article.Where(x => x.userName.Equals(viewUsername)).ToList();
                ViewBag.docName = generaldata.FirstOrDefault().name;
                ViewBag.workLocation = docdata.FirstOrDefault().workLocation;
                ViewBag.description = docdata.FirstOrDefault().description;
                ViewBag.username = viewUsername;

                //If logged in user is viewing own profile, let ViewBag.ownAccount == true, show the create article button, show edit profile button
                if (username == viewUsername)
                {
                    ViewBag.ownAccount = true;
                }
                //Else, if logged in user is viewing other's profile, create article button not available, edit profile button not avilable
                else
                {
                    ViewBag.ownAccount = false;
                }

            }

            return View(displayList.ToPagedList(pageNumber, pageSize));
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

        public ActionResult EditDrProfile()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }
            else
            {
                //Get username of current user logged in
                string username = Session["username"].ToString();

                //Get info of profile that is being viewed
                var generaldata = smokeFreeDB.GeneralUser.Where(x => x.userName.Equals(username));
                var docdata = smokeFreeDB.Doctor.Where(x => x.userName.Equals(username));

                ViewBag.workLocation = docdata.FirstOrDefault().workLocation;
                ViewBag.description = docdata.FirstOrDefault().description;
                ViewBag.contactNo = docdata.FirstOrDefault().contactNo;
                ViewBag.username = username;

            }
           

            return View();

        }

        public ActionResult EdittedDrProfile(DoctorCompoundModel accountInfo)
        {
            string username = Session["username"].ToString();
            var doctor = smokeFreeDB.Doctor.Find(username);
            doctor.description = accountInfo.Doctors.description;
            doctor.contactNo = accountInfo.Doctors.contactNo;
            doctor.workLocation = accountInfo.Doctors.workLocation;
            smokeFreeDB.SaveChanges();

            return RedirectToAction("DrProfile", new {viewUsername = username, page = 1});
        }
    }
}