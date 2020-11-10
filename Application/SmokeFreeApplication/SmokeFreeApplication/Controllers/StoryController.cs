using SmokeFreeApplication.Models;
using System.Web.Mvc;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;
using PagedList;

namespace SmokeFreeApplication.Controllers
{
    public class StoryController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        private TagController tagAccess = new TagController();

        // search is implement at index method, stories (in this case)
        public ActionResult Stories(string option,string search, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            List<Story> displayList = new List<Story>();
            ViewBag.option = option;
            ViewBag.search = search;
            ViewBag.userType = Session["docOrMember"].ToString();
            // Session["docOrMember"] = "doc";
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            if (!String.IsNullOrEmpty(search))
            {
                search.Replace(' ', '+');
                if (option == "Name")
                {
                    displayList = smokeFreeDB.Story.Where(x => x.title.Contains(search) || search == null).OrderByDescending(x=>x.postDate).ToList();
                    ViewBag.DisplayStatus = "No results found.";
                }
                else if (option == "Tags")
                {
                    displayList = tagAccess.searchStoryByTag(search);
                    ViewBag.DisplayStatus = "No results found.";
                }

            }
            else
            {
                displayList =  smokeFreeDB.Story.OrderByDescending(x => x.postDate).ToList();
            }


            return View(displayList.ToPagedList(pageNumber, pageSize));

        }

        public FileContentResult retrieveUserPic(string username)
        {
            byte[] imgByteArray = smokeFreeDB.GeneralUser.Find(username).profilePicture;
            if(imgByteArray != null)
            {
                return new FileContentResult(imgByteArray, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
        public ActionResult ViewStory(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
            ViewBag.tagList = tagAccess.getTags(id);
            Story story = smokeFreeDB.Story.Find(id);
            ViewBag.storyDetails = story.body;
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        public ActionResult CreateStory()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStory([Bind(Include = "storyID,userName,title, body,postDate")] Models.Story story)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            if (ModelState.IsValid)
            {
                story.postDate = DateTime.Now;
                story.userName = Session["username"].ToString();
                string tags = Request["tagsinput"].ToString();
                smokeFreeDB.Story.Add(story);
                smokeFreeDB.SaveChanges();
                smokeFreeDB.Entry(story).Reload();
                tagAccess.saveTags(tags, story.storyID);
                return RedirectToAction("Stories");
            }

            return RedirectToAction("Stories");
        }


    }
}