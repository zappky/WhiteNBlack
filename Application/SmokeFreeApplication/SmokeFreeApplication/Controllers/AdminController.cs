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

        private ActionResult ValidateAdminLogin()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInMember", "Account");
            }

            return View();
        }
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //Default view of the admin interface
        public ActionResult Manage()
        {
            //ValidateAdminLogin();

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


        public ActionResult ArticleManage(ArticleQuery q)
        {
            //var article = from c in smokeFreeDB.Article
            //               where c.articleID == q.id
            //               select c;
            //return View(article.ToList()[0]);
            var article = smokeFreeDB.Article.Find(q.id);
            return View(article);
        }

        public ActionResult DoctorManage(DocQuery q)
        {
            /*
            //Primary key seems to be not doctorID somehow?
            var doctor = from c in smokeFreeDB.Doctor
                           where c.userName == q.id
                           select c;
            return View(doctor.ToList()[0]);
            */
            //var doctor = smokeFreeDB.Doctor.Find(q.id);
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            return View(aDoc);
        }

        public ActionResult ClosePost(ArticleQuery q)
        {
            return RedirectToAction("Manage");
        }
        public ActionResult ApprovePost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            //smokeFreeDB.Article.Remove(article);
            article.articleStatus = "approved";
            //smokeFreeDB.Article.Add(article);
            smokeFreeDB.SaveChanges();

            return RedirectToAction("Manage");
        }
        public ActionResult RejectPost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            //article.articleStatus = "rejected";
            smokeFreeDB.Article.Remove(article);
            smokeFreeDB.SaveChanges();
            return RedirectToAction("Manage");
        }


        public ActionResult CloseDoc(DocQuery q)
        {
            return RedirectToAction("Manage");
        }
        public ActionResult ApproveDoc(DocQuery q)
        {
            /*
            //Primary key violation,so not working
            var doctor = from c in smokeFreeDB.Doctor
                         where c.userName == q.id
                         select c;
            var aDoc = doctor.ToList()[0];
            */
            var aDoc = smokeFreeDB.Doctor.Find(q.id);

            //smokeFreeDB.Doctor.Remove(aDoc);
            aDoc.adminVerify = true;
            //smokeFreeDB.Doctor.Add(aDoc);
            smokeFreeDB.SaveChanges();

            return RedirectToAction("Manage");
        }
        public ActionResult RejectDoc(DocQuery q)
        {
            /*
            //Primary key violation,so not working
            var doctor = from c in smokeFreeDB.Doctor
                         where c.doctorID == q.id
                         select c;
            var aDoc = doctor.ToList()[0];
            */
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            aDoc.adminVerify = false;
            //smokeFreeDB.Doctor.Remove(aDoc);
            smokeFreeDB.SaveChanges();
            return RedirectToAction("Manage");
        }




        public ActionResult BoardcastMessage()
        {
            //spawn a Message dialong box
            return View();
        }

        public FileContentResult retrieveUserPic(string username)
        {
            /*
            byte[] imgByteArray = smokeFreeDB.Article.Find(username).articlePicture;
            if (imgByteArray != null)
            {
                return new FileContentResult(imgByteArray, "image/jpeg");
            }
            else
            {
                return null;
            }
            */
            return null;
        }

    }

    public class ArticleQuery
    {
        public int id { get; set; }

        public ArticleQuery(int i)
        {
            id = i;
        }
        public ArticleQuery()
        {
            id = 0;
        }
    }

    public class DocQuery
    {
        public string id { get; set; }

        public DocQuery(string i)
        {
            id = i;
        }
        public DocQuery()
        {
            id = "";
        }
    }
}