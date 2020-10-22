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
            var article = smokeFreeDB.Article.Find(q.id);
            return View(article);
        }

        public ActionResult DoctorManage(DocQuery q)
        {
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            return View(aDoc);
        }
        public ActionResult PrevPost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            var nextarticle = smokeFreeDB.Article.Find(q.id - 1);
            if (nextarticle != null)
            {
                while ((nextarticle != null) && (nextarticle.articleStatus != article.articleStatus) )
                {
                    nextarticle = smokeFreeDB.Article.Find(nextarticle.articleID - 1);
                }
                if ((nextarticle != null) && (nextarticle.articleStatus == article.articleStatus))
                    return RedirectToAction("ArticleManage", new SmokeFreeApplication.Controllers.ArticleQuery(nextarticle.articleID));
            }

            return RedirectToAction("ArticleManage", new SmokeFreeApplication.Controllers.ArticleQuery(article.articleID));
        }
        public ActionResult NextPost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            var nextarticle = smokeFreeDB.Article.Find(q.id + 1);
            if (nextarticle != null)
            {
                while ((nextarticle != null) && (nextarticle.articleStatus != article.articleStatus)  )
                {
                    nextarticle = smokeFreeDB.Article.Find(nextarticle.articleID + 1);
                }
                if ((nextarticle != null) && (nextarticle.articleStatus == article.articleStatus))
                    return RedirectToAction("ArticleManage", new SmokeFreeApplication.Controllers.ArticleQuery(nextarticle.articleID));
            }

            return RedirectToAction("ArticleManage", new SmokeFreeApplication.Controllers.ArticleQuery(article.articleID));
        }

        public ActionResult ClosePost(ArticleQuery q)
        {
            return RedirectToAction("Manage");
        }
        public ActionResult ApprovePost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            article.articleStatus = "approved";
            smokeFreeDB.SaveChanges();

            return RedirectToAction("Manage");
        }
        public ActionResult RejectPost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
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
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            aDoc.adminVerify = true;
            smokeFreeDB.SaveChanges();

            return RedirectToAction("Manage");
        }

        public ActionResult RejectDoc(DocQuery q)
        {
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            aDoc.adminVerify = false;
            smokeFreeDB.SaveChanges();

            return RedirectToAction("Manage");
        }
        public ActionResult PrevDoc(DocQuery q)
        {
            var doc = smokeFreeDB.Doctor.Find(q.id);
            Doctor nextdoc = doc;
            var docList = smokeFreeDB.Doctor.ToList();
            var docIndex  = docList.IndexOf(doc);
            if(docIndex - 1 >= 0)
               nextdoc = docList[docIndex - 1];

            if (nextdoc != null)
            {
                while ((nextdoc.adminVerify != doc.adminVerify) && (docIndex - 1 >= 0))
                {
                    docIndex--;
                    nextdoc = docList[docIndex];

                }
                if (nextdoc.adminVerify == doc.adminVerify)
                    return RedirectToAction("DoctorManage", new SmokeFreeApplication.Controllers.DocQuery(nextdoc.userName));
            }

            return RedirectToAction("DoctorManage", new SmokeFreeApplication.Controllers.DocQuery(doc.userName));
        }
        public ActionResult NextDoc(DocQuery q)
        {
            var doc = smokeFreeDB.Doctor.Find(q.id);
            Doctor nextdoc = doc;
            var docList = smokeFreeDB.Doctor.ToList();
            var docIndex = docList.IndexOf(doc);
            if (docIndex + 1 < docList.Count())
                nextdoc = docList[docIndex + 1];
            if (nextdoc != null)
            {
                while ((nextdoc.adminVerify != doc.adminVerify) && (docIndex + 1 < docList.Count()))
                {
                    docIndex++;
                    nextdoc = docList[docIndex];

                }
                if (nextdoc.adminVerify == doc.adminVerify)
                    return RedirectToAction("DoctorManage", new SmokeFreeApplication.Controllers.DocQuery(nextdoc.userName));
            }

            return RedirectToAction("DoctorManage", new SmokeFreeApplication.Controllers.DocQuery(doc.userName));
        }

        public ActionResult BoardcastMessage()
        {
            //spawn a boardcast form
            return View();
        }

        [HttpPost]
        public ActionResult BroadcastForm(Models.BroadcastMessage m)
        {
            m.ownerName = Session["username"].ToString();
            m.postTime = DateTime.Now;
            m.neverShowAgain = false;
            //not sure if need to set id
            m.id = 10;
            //Bug: DB doesnt seems to save the new entry
            //EDIT: it does appear in db table, but only after i went on to do other stuff
            //Not sure what is going on
            smokeFreeDB.BroadCastMessage.Add(m);
            smokeFreeDB.SaveChanges();

            return RedirectToAction("Manage");
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

        public DocQuery(string i="")
        {
            id = i;

        }
        public DocQuery()
        {
            id = "";
        }
    }
}