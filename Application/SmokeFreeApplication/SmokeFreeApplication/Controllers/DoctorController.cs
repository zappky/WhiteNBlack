using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmokeFreeApplication.Models;
using PagedList;
using System.IO;

namespace SmokeFreeApplication.Controllers
{
    public class DoctorController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        public ActionResult Index()
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

        //viewUsername = username of the profile you want to view
        public ActionResult DrProfile(string viewUsername, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            List<Article> displayList = new List<Article>();

            //If null, redirect user back to sign in page
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
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
                ViewBag.contactEmail = generaldata.FirstOrDefault().email;
                ViewBag.contactPhone = docdata.FirstOrDefault().contactNo;
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


        public ActionResult EditDrProfile()
        {
            DoctorCompoundModel docModel = new DoctorCompoundModel();
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                //Get username of current user logged in
                string username = Session["username"].ToString();

                //Get info of profile that is being viewed
                var generaldata = smokeFreeDB.GeneralUser.Where(x => x.userName.Equals(username));
                var docdata = smokeFreeDB.Doctor.Where(x => x.userName.Equals(username));

                docModel.Doctors = docdata.FirstOrDefault();
                docModel.GeneralUsers = generaldata.FirstOrDefault();

                ViewBag.username = username;

            }
           

            return View(docModel);

        }

        public ActionResult EdittedDrProfile(DoctorCompoundModel accountInfo)
        {
            string username = Session["username"].ToString();
            var doctor = smokeFreeDB.Doctor.Find(username);
            var generalUser = smokeFreeDB.GeneralUser.Find(username);

            HttpPostedFileBase postedFile = Request.Files["ImageFile"];
            //If user uploads new profile picture
            if (postedFile.ContentLength > 0 && postedFile.ContentType.Contains("image"))
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] img = binaryReader.ReadBytes((int)stream.Length);
                generalUser.profilePicture = img;
                
            }

            doctor.description = accountInfo.Doctors.description;
            doctor.contactNo = accountInfo.Doctors.contactNo;
            doctor.workLocation = accountInfo.Doctors.workLocation;
            smokeFreeDB.Configuration.ValidateOnSaveEnabled = false;
            smokeFreeDB.SaveChanges();

            return RedirectToAction("DrProfile", new {viewUsername = username, page = 1});
        }
    }
}