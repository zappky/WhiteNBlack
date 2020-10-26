using SmokeFreeApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

using System.Net.Mail;
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

        private List<AdminManageDataPacket> populateView(string option, string search)
        {
            AdminManageDataPacket allList = new AdminManageDataPacket() { list1 = smokeFreeDB.Article.ToList(), list2 = smokeFreeDB.Doctor.ToList() };
            List<AdminManageDataPacket> dataPacket = new List<AdminManageDataPacket>() { allList };

            if (!String.IsNullOrEmpty(search))
            {
                AdminManageDataPacket searchList = new AdminManageDataPacket() { };
                switch (option)
                {

                    case "byArticle":
                        searchList.list1 = smokeFreeDB.Article.Where(x => x.title.Contains(search) || search == null).ToList();
                        break;
                    case "byDoctor":
                        searchList.list2 = smokeFreeDB.Doctor.Where(x => x.userName.Contains(search) || search == null).ToList();
                        break;

                    case "byAll":
                        searchList.list1 = smokeFreeDB.Article.Where(x => x.title.Contains(search) || search == null).ToList();
                        searchList.list2 = smokeFreeDB.Doctor.Where(x => x.userName.Contains(search) || search == null).ToList();
                        break;
                    default:
                        break;
                }
                dataPacket.Add(searchList);
            }
            return dataPacket;
        }

        //Default view of the admin interface
        public ActionResult Manage(string option, string search)
        {
            return View(populateView(option,search));
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
            emailableModel<Article> aArticle = new emailableModel<Article>();
            aArticle.model = smokeFreeDB.Article.Find(q.id);
            return View(aArticle);
        }

        public ActionResult DoctorManage(DocQuery q)
        {
            emailableModel<Doctor> aDoc = new emailableModel<Doctor>();
            aDoc.model = smokeFreeDB.Doctor.Find(q.id);
            return View(aDoc);
        }
        public ActionResult PrevPost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            var articles = from a in smokeFreeDB.Article
                           where a.articleStatus == article.articleStatus
                           select a;

           var articlesList =  articles.ToList();
            int index = articlesList.FindIndex(a => a.articleID == q.id);

            var nextarticle = article;
            if (index - 1 >= 0)
            {
                nextarticle = articlesList.ElementAt(--index);
            }

            return RedirectToAction("ArticleManage", new SmokeFreeApplication.Controllers.ArticleQuery(nextarticle.articleID));
        }
        public ActionResult NextPost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            var articles = from a in smokeFreeDB.Article
                           where a.articleStatus == article.articleStatus
                           select a;

            var articlesList = articles.ToList();
            int index = articlesList.FindIndex(a => a.articleID == q.id);
            var nextarticle = article;
            if (index + 1 < articlesList.Count())
            {
                nextarticle = articlesList.ElementAt(++index);
            }


            return RedirectToAction("ArticleManage", new SmokeFreeApplication.Controllers.ArticleQuery(nextarticle.articleID));
        }

        public ActionResult ClosePost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            if (article.articleStatus == "approved")
            {
                ViewBag.activeTabContent = "Approved Article";
            }
            else
            {
                ViewBag.activeTabContent = "Pending Article";
            }
            
            return View("Manage", populateView("",""));
        }
        public ActionResult ApprovePost(ArticleQuery q)
        {
            var article = smokeFreeDB.Article.Find(q.id);
            article.articleStatus = "approved";
            smokeFreeDB.SaveChanges();

            ViewBag.activeTabContent = "Pending Article";
            return View("Manage", populateView("", ""));
        }
        [HttpPost]
        public ActionResult RejectPost(emailableModel<Article> aModel)
        {
            EmailMessage theMail = aModel.emailModel;
            Article theArticle = aModel.model;
            var article = smokeFreeDB.Article.Find(theArticle.articleID);


            try
            {
                MailMessage mail = new MailMessage();
                //string testEmail = "pang_kee_yang@hotmail.com";
                mail.To.Add(theMail.To);
                mail.From = new MailAddress(theMail.From);
                if (String.IsNullOrEmpty(theMail.Subject))
                    mail.Subject = "[Rejected article] Thank you for your submission";
                else
                    mail.Subject = theMail.Subject;
                if (String.IsNullOrEmpty(theMail.Body))
                    mail.Body = "Hi Sir/Mdm,\n Sorry, we have deem it unfit to be posted. Please submit another one\n Thank you";
                else
                    mail.Body = theMail.Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("zappiky@gmail.com", "Omaewamou"); // Enter senders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);

                smokeFreeDB.Article.Remove(article);
                smokeFreeDB.SaveChanges();
            }
            catch(Exception e)
            {
                //hmmm....
            }

            ViewBag.activeTabContent = "Pending Article";
            return View("Manage", populateView("", ""));
        }


        public ActionResult CloseDoc(DocQuery q)
        {
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            if (aDoc.adminVerify == true)
            {
                ViewBag.activeTabContent = "Approved Doctor";
            }
            else
            {
                ViewBag.activeTabContent = "Pending Doctor";
            }
            return View("Manage", populateView("", ""));
            //return RedirectToAction("Manage");
        }
        public ActionResult ApproveDoc(DocQuery q)
        {
            var aDoc = smokeFreeDB.Doctor.Find(q.id);
            aDoc.adminVerify = true;
            try
            {

                smokeFreeDB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            


            ViewBag.activeTabContent = "Pending Doctor";
            return View("Manage", populateView("", ""));
        }
        [HttpPost]
        public ActionResult RejectDoc(emailableModel<Doctor> aModel)
        {
            EmailMessage theMail = aModel.emailModel;
            Doctor theDoc = aModel.model;
            var aDoc = smokeFreeDB.Doctor.Find(theDoc.userName);

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(theMail.To);
                mail.From = new MailAddress(theMail.From);
                if (String.IsNullOrEmpty(theMail.Subject))
                    mail.Subject = "[Rejected Doctor] Thank you for your registration";
                else
                    mail.Subject = theMail.Subject;
                if (String.IsNullOrEmpty(theMail.Body))
                    mail.Body = "Hi Sir/Mdm,\n Sorry, we have deem you unfit to be accepted. Please submit another one request\n Thank you";
                else
                    mail.Body = theMail.Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("zappiky@gmail.com", "Omaewamou"); // Enter senders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);

                
                smokeFreeDB.Doctor.Remove(aDoc);
                smokeFreeDB.SaveChanges();

            }
            catch (Exception e)
            {
                //hmmm....
            }

            ViewBag.activeTabContent = "Pending Doctor";
            return View("Manage", populateView("", ""));
        }
        public ActionResult PrevDoc(DocQuery q)
        {
            var doc = smokeFreeDB.Doctor.Find(q.id);
            var docs = from a in smokeFreeDB.Doctor
                           where a.adminVerify == doc.adminVerify
                           select a;
            var docList = docs.ToList();


            var docIndex  = docList.IndexOf(doc);
            Doctor nextdoc = doc;
            if (docIndex - 1 >= 0)
               nextdoc = docList[--docIndex];

            return RedirectToAction("DoctorManage", new SmokeFreeApplication.Controllers.DocQuery(nextdoc.userName));
        }
        public ActionResult NextDoc(DocQuery q)
        {
            var doc = smokeFreeDB.Doctor.Find(q.id);
            var docs = from a in smokeFreeDB.Doctor
                       where a.adminVerify == doc.adminVerify
                       select a;
            var docList = docs.ToList();


            var docIndex = docList.IndexOf(doc);
            Doctor nextdoc = doc;
            if (docIndex + 1 < docList.Count())
                nextdoc = docList[++docIndex];

            return RedirectToAction("DoctorManage", new SmokeFreeApplication.Controllers.DocQuery(nextdoc.userName));
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
            //m.id = 10;
 
            smokeFreeDB.BroadCastMessage.Add(m);
            smokeFreeDB.SaveChanges();

            return View("Manage", populateView("", ""));
        }


        public FileContentResult retrieveUserPic(string username)
        {
            /*
            byte[] imgByteArray = smokeFreeDB.Article.Find(username).articlePicture;
            if (imgByteArray != null)
            {
                return new FileContentResult(imgByteArray, "image/jpeg");
            }
            */
            return null;
        }


    }

    public class emailableModel<T>
    {
        public T model { get; set; }
        public EmailMessage emailModel { get; set; }
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