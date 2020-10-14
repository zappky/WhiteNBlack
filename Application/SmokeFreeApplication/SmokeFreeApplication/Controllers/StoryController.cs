using SmokeFreeApplication.Models;
using System.Web.Mvc;
using System.Net;
using System;
using System.Linq;
using SmokeFreeApplication.Migrations;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;

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
                string tags = Request["tagsinput"].ToString();
                smokeFreeDB.Story.Add(story);
                smokeFreeDB.SaveChanges();
                smokeFreeDB.Entry(story).Reload();
                saveTags(tags, story.storyID);
                return RedirectToAction("Stories");
            }

            return RedirectToAction("Stories");
        }
        public void saveTags(string inputTags, int storyID)
        {
            string[] tagArray = inputTags.Split(',');
            Tag[] tagList = smokeFreeDB.Tag.ToArray();
            for (int i = 0; i < tagArray.Length; i++)
            {
                string tmp = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x=>x.tagName == tmp).FirstOrDefault();
                StoriesTag storyTag = new StoriesTag();
                if (tag != null)
                {
                    // Tag is found
                    storyTag.tagID = tag.tagID;
                    storyTag.storyID = storyID;
                    smokeFreeDB.StoriesTag.Add(storyTag);
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
                    storyTag.tagID = newTag.tagID;
                    storyTag.storyID = storyID;
                    smokeFreeDB.StoriesTag.Add(storyTag);
                    smokeFreeDB.SaveChanges();

                }
            }


        }

        public ActionResult PostComment(int? id)
        {
            // Calls CommentsController to make a story comment with some variables provided
            Story story = smokeFreeDB.Story.Find(id);
            return View(story);
        }
    }
}