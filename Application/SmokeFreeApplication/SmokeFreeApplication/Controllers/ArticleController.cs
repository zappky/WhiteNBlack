using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using SmokeFreeApplication.Models;
using PagedList;


namespace SmokeFreeApplication.Controllers
{
    public class ArticleController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();

        // GET: Article
        public ActionResult Articles()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(smokeFreeDB.Story.ToList().ToPagedList(pageNumber, pageSize));
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
        public ActionResult ViewArticle(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = smokeFreeDB.Story.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

    }

}