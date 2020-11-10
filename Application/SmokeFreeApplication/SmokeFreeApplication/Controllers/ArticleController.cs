﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SmokeFreeApplication.Models;
using SmokeFreeApplication.Controllers;
using PagedList;
using System.Net;
using System.IO;

namespace SmokeFreeApplication.Controllers
{
    public class ArticleController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        private TagController tagAccess = new TagController();

        // GET: Article
        public ActionResult Articles(string option, string search, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            List<Article> displayList = new List<Article>();
            ViewBag.search = search;
            ViewBag.option = option;
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            if (!String.IsNullOrEmpty(search))
            {
                search.Replace(' ', '+');
                if (option == "Name")
                {
                    displayList = smokeFreeDB.Article.Where(x => x.title.Contains(search) || search == null && x.articleStatus == "approved").ToList();
                    ViewBag.DisplayStatus = "No results found.";
                }
                else if (option == "Tags")
                {
                    displayList = tagAccess.searchArticleByTag(search);
                    ViewBag.DisplayStatus = "No results found.";
                }

            }
            else
            {
                displayList = smokeFreeDB.Article.Where(x => x.articleStatus == "approved").ToList();
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

        public FileContentResult retrieveArticlePic(int articleID)
        {
            byte[] imgByteArray = smokeFreeDB.Article.Find(articleID).articlePicture;
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
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.tagList = tagAccess.getArticleTags(id);
            Article article = smokeFreeDB.Article.Find(id);
            ViewBag.articleDetails = article.body;
 
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        public ActionResult DeleteArticle(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

            try
            {

                var article = smokeFreeDB.Article.Find(id);
                if (article != null) {
                    tagAccess.deleteArticleTags(id);
                    smokeFreeDB.Article.Remove(article);
                }

                smokeFreeDB.SaveChanges();

                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return View();
            }
        }


        public ActionResult createArticle()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult createArticle([Bind(Include = "articleID, articleStatus, userName, title, body,postDate")] Models.Article article)
        {
            string username = Session["username"].ToString();
            var doctor = smokeFreeDB.Doctor.Find(username);
            var generalUser = smokeFreeDB.GeneralUser.Find(username);
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            if (ModelState.IsValid)
            {
                //Convert uploaded file to byte[]
                HttpPostedFileBase postedFile = Request.Files["ImageFile"];
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] img = binaryReader.ReadBytes((int)stream.Length);

                article.postDate = DateTime.Now;
                article.userName = Session["username"].ToString();
                article.articleStatus = "pending";
                string tags = Request["tagsinput"].ToString();
                article.articlePicture = img;
                //smokeFreeDB.Configuration.ValidateOnSaveEnabled = false;
                smokeFreeDB.Article.Add(article);
                smokeFreeDB.SaveChanges();
                smokeFreeDB.Entry(article).Reload();
                tagAccess.saveArticleTags(tags, article.articleID);
                return RedirectToAction("DrProfile","Doctor", new { viewUsername = username, page = 1 });
            }

            return RedirectToAction("DrProfile", "Doctor", new { viewUsername = username, page = 1 });
        }

    }

    
}