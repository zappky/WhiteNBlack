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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        //When i click ManageStories
        //Brings me to main admin interface
        public ActionResult ClickManageArticles()
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

        //When i click on a request
        //View post management
        //probably need some agrgument to be able to get the correct post to view
        public ActionResult RequestView()
        {
            //dummy artcile

            Article aArticle = new Article();
            aArticle.id = 1;

            DocEntry aUser = new DocBuilder()
            .UserBio("hahadoc", new DateTime(), "female")
            .UserMeta("userHahaDoc", "abe@hotmail.com", "kekeke", "something")
            .DocInfo("ntu", "fake", 999, "999", true)
            .Build();

            aArticle.owner = aUser;
            aArticle.texts = "Dame Da yo";
            aArticle.Title = "Baka Mitai";
            return View(aArticle);
        }

        //When i click on a Rejected
        //View post managment(rejection)
        //probably need some agrgument to be able to get the correct post to view
        public ActionResult RejectedView()
        {
            //dummy artcile

            Article aArticle = new Article();
            aArticle.id = 1;

            DocEntry aUser = new DocBuilder()
            .UserBio("hahadoc", new DateTime(), "female")
            .UserMeta("userHahaDoc", "abe@hotmail.com", "kekeke", "something")
            .DocInfo("ntu", "fake", 999, "999", true)
            .Build();

            aArticle.owner = aUser;
            aArticle.texts = "Dame Da yo";
            aArticle.Title = "Baka Mitai";
            return View(aArticle);
        }

        //When click on accepted post
        //just go to the viewing of the post as per normal. 


        //Default view of the admin interface
        public ActionResult Manage()
        {
            return View();
        }


        //When i click CLOSE, on post management view
        public ActionResult ClosePost()
        {
            //discard changes
            //go back Admin interface
            return RedirectToAction("Manage");
        }


        //FUNCTION BELOW are probably wrong. They are probably needed to be done on the cshtml file itself



        //When i click Reject, on post management view
        public ActionResult RejectPost()
        {
            //spawn a reject confirmation dialog box
            return View();
        }

        //When i click Publish, on post management view
        public ActionResult PublishPost()
        {
            //pulish the post
            //add into Content manager?
            //add into database?
            return View();
        }

        //When i click view retract , on post management view
        public ActionResult RetractReject()
        {
            //Unban the post?
            return View();
        }

        //When i click view reject reason, on post management view
        public ActionResult ViewRejectReason()
        {
            //spawn dialog box containing reason for rejected post
            return View();
        }
    }
}