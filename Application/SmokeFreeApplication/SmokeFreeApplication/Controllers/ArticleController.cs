using System;
using System.Collections.Generic;
using System.Linq;
using SmokeFreeApplication.Migrations;
using Microsoft.SqlServer.Server;
using System.Web.Mvc;
using SmokeFreeApplication.Models;
using PagedList;
using System.Net;

namespace SmokeFreeApplication.Controllers
{
    public class ArticleController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();

        // GET: Article
        public ActionResult Articles(string option, string search, int? page)
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            List<Article> displayList = new List<Article>();
            ViewBag.search = search;
            ViewBag.option = option;

            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }
            if (!String.IsNullOrEmpty(search))
            {
                if (option == "Name")
                {
                    displayList = smokeFreeDB.Article.Where(x => x.title.Contains(search) || search == null).ToList();
                }
                else if (option == "Tags")
                {
                    displayList = searchTags(search);
                }

            }
            else
            {
                displayList = smokeFreeDB.Article.ToList();
            }


            return View(displayList.ToPagedList(pageNumber, pageSize));
        }

        //search with tags
        public List<Article> searchTags(string inputTags)
        {
            string[] tagArray = inputTags.Split(',');
            string tempTagName;
            int tempId;
            ArticlesTag article;
            int[] tagIdArray = new int[tagArray.Length];
            List<Article> displayList = new List<Article>();
            for (int i = 0; i < tagArray.Length; i++)
            {
                tempTagName = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tempTagName).FirstOrDefault();
                if (tag != null)
                {
                    tagIdArray[i] = tag.tagID;
                }
            }
            for (int i = 0; i < tagIdArray.Length; i++)
            {
                tempId = tagIdArray[i];
                List<ArticlesTag> articleTags = smokeFreeDB.ArticlesTag.Where(x => x.tagID == tempId).ToList();
                for (int j = 0; j < articleTags.Count; j++)
                {
                    tempId = articleTags[j].articleID;
                    displayList.Add(smokeFreeDB.Article.Where(x => x.articleID == tempId).FirstOrDefault());
                }

            }
            return displayList;
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
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.tagList = getTags(id);
            Article article = smokeFreeDB.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public List<String> getTags(int? id)
        {
            List<ArticlesTag> articleTags = smokeFreeDB.ArticlesTag.Where(x => x.articleID == id).ToList();
            List<String> displayList = new List<string>();
            Tag tagItem;
            int temp;
            for (int i = 0; i < articleTags.Count; i++)
            {
                temp = articleTags[i].tagID;
                tagItem = smokeFreeDB.Tag.Where(x => x.tagID == temp).FirstOrDefault();
                displayList.Add(tagItem.tagName);
            }
            return displayList;
        }

        public ActionResult CreateArticle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle([Bind(Include = "articleID,userName,title, body,postDate")] Models.Article article)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInDoctor", "Account");
            }
            if (ModelState.IsValid)
            {
                article.postDate = DateTime.Now;
                article.userName = Session["username"].ToString();
                string tags = Request["tagsinput"].ToString();
                smokeFreeDB.Article.Add(article);
                smokeFreeDB.SaveChanges();
                smokeFreeDB.Entry(article).Reload();
                saveTags(tags, article.articleID);
                return RedirectToAction("Articles");
            }

            return RedirectToAction("Articles");
        }

        public void saveTags(string inputTags, int articleID)
        {
            string[] tagArray = inputTags.Split(',');
            Tag[] tagList = smokeFreeDB.Tag.ToArray();
            for (int i = 0; i < tagArray.Length; i++)
            {
                string tmp = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tmp).FirstOrDefault();
                ArticlesTag articleTag = new ArticlesTag();
                if (tag != null)
                {
                    // Tag is found
                    articleTag.tagID = tag.tagID;
                    articleTag.articleID = articleID;
                    smokeFreeDB.ArticlesTag.Add(articleTag);
                    smokeFreeDB.SaveChanges();
                }
                else
                {
                    //Tag is not found in database
                    Tag newTag = new Tag();
                    newTag.tagName = tagArray[i];
                    smokeFreeDB.Tag.Add(newTag);
                    smokeFreeDB.SaveChanges();
                    smokeFreeDB.Entry(newTag).Reload();
                    articleTag.tagID = newTag.tagID;
                    articleTag.articleID = articleID;
                    smokeFreeDB.ArticlesTag.Add(articleTag);
                    smokeFreeDB.SaveChanges();

                }
            }
        }
        // Borrow code from ray!
    }

    
}