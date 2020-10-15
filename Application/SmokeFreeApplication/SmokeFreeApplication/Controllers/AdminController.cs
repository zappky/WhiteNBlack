using SmokeFreeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The controller class for admin interface
/// This will contains the action for that page
/// to manage request, manage accepted,manage rejected
/// </summary>

namespace SmokeFreeApplication.Controllers
{
    public class AdminController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //Default view of the admin interface
        public ActionResult Manage()
        {
            //return View();
            return View( new AdminManageDataPacket() { list1 = smokeFreeDB.Article.ToList(), list2 = smokeFreeDB.Doctor.ToList() });
        }

        //When i click My Admin
        //Brings me to main admin interface
        public ActionResult MyAdmin()
        {
            return RedirectToAction("Manage");
        }

        //When i click Logout
        //Brings me to home? and clear the session?
        public ActionResult ClickLogOut()
        {

            Session.Clear();
            return View("../Home/Index");

        }
        private DocEntry CreateDummyDoctor()
        {
            DocEntry aUser = new DocBuilder()
            .UserBio("hahadoc", new DateTime(), "female")
            .UserMeta("userHahaDoc", "abe@hotmail.com", "kekeke", "something")
            .DocInfo("ntu", "fake", 999, "999", true)
            .Build();

            return aUser;
        }
        private Article CreateDummyArticle()
        {
            //dummy artcile

            Article aArticle = new Article();
            aArticle.articleID = 1;

            aArticle.userName = "1";
            aArticle.body = "Dame Da yo";
            aArticle.title = "Baka Mitai";

            return aArticle;
        }

        //When i click on a request
        //View post management
        //probably need some agrgument to be able to get the correct post to view
        public ActionResult PendingArticleView()
        {
            /*
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInMember", "Account");
            }
            */

            return View(smokeFreeDB.Article.ToList());
        }


        //When i click on a request
        //View post management
        //probably need some agrgument to be able to get the correct post to view
        public ActionResult PendingDoctorView()
        {


            return View(CreateDummyDoctor());
        }

        public ActionResult ApprovedArticleView()
        {
            //display list of appproved article from db
            return View(CreateDummyArticle());
        }

        public ActionResult ApprovedDoctorView()
        {
            //display list of appproved doctor from db
            return View(CreateDummyDoctor());
        }


        //When i click CLOSE, on post management view
        public ActionResult ClosePost()
        {
            //discard changes
            //go back Admin interface
            return RedirectToAction("Manage");
        }

        //When i click CLOSE, on post management view
        public ActionResult CloseDoctor()
        {
            //discard changes
            //go back Admin interface
            return RedirectToAction("Manage");
        }


        //FUNCTION BELOW are probably wrong. They are probably needed to be done on the cshtml file itself

        //When i click Approve, on post management view
        public ActionResult ApprovePost()
        {
            //pulish the post
            //add into Content manager?
            //add into database?
            return View();
        }

        //When i click Reject, on post management view
        public ActionResult RejectPost()
        {
            //spawn a reject confirmation dialog box
            return View();
        }


        public ActionResult ApproveDoctor()
        {
            return View();
        }

        //When i click Reject, on doctor management view
        public ActionResult RejectDoctor()
        {
            //spawn a reject confirmation dialog box
            return View();
        }

        public ActionResult BoardcastMessage()
        {
            //spawn a Message dialong box
            return View();
        }


  

    }
}