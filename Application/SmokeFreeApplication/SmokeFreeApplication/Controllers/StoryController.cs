using SmokeFreeApplication.Models;
using System.Web.Mvc;
using System.Net;
using System;
using System.Linq;
using SmokeFreeApplication.Migrations;
using Microsoft.SqlServer.Server;

namespace SmokeFreeApplication.Controllers
{
    public class StoryController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();

        // GET: Story
        public ActionResult Stories()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInMember", "Account");
            }


            return View(smokeFreeDB.Story.ToList());
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
 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
            Story story = smokeFreeDB.Story.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }
        public ActionResult CreateStory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStory([Bind(Include = "storyID,userName,title, body,postDate")] Models.Story story)
        {
            if (ModelState.IsValid)
            {
                story.postDate = DateTime.Now;
                story.userName = Session["username"].ToString();
                smokeFreeDB.Story.Add(story);
                smokeFreeDB.SaveChanges();
                return RedirectToAction("Stories");
            }

            return RedirectToAction("Stories");
        }
    }
}