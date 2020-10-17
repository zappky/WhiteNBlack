using SmokeFreeApplication.Models;
using System.Web.Mvc;
using System.Net;
using System;
using System.Linq;
using SmokeFreeApplication.Migrations;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using PagedList;

namespace SmokeFreeApplication.Controllers
{
    public class StoryController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();

        // GET: Story
        public ActionResult Stories(string option,string search, int? page)
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            List<Story> displayList = new List<Story>();
            ViewBag.search = search;

            if (Session["username"] == null)
            {
                return RedirectToAction("SignInMember", "Account");
            }
            if (option == "Name")
            {
                ViewBag.searchType = "None";
            }
            else if (option == "Tags")
            {
                ViewBag.searchType = "tagsinput";
            }
            if (!String.IsNullOrEmpty(search))
            {
                if (option == "Name")
                {
                    displayList = smokeFreeDB.Story.Where(x => x.title.Contains(search) || search == null).ToList();
                }
                else if (option == "Tags")
                {
                    displayList = searchTags(search);
                }

            }
            else
            {
                displayList = smokeFreeDB.Story.ToList();
            }


            return View(displayList.ToPagedList(pageNumber, pageSize));

        }
        public void setSearchBar()
        {

        }
        public List<Story> searchTags(string inputTags)
        {
            string[] tagArray = inputTags.Split(',');
            string tempTagName;
            int tempId;
            StoriesTag story;
            int[] tagIdArray= new int[tagArray.Length];
            List<Story> displayList = new List<Story>();
            for (int i = 0; i < tagArray.Length; i++)
            {
                tempTagName = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tempTagName).FirstOrDefault();
                if(tag != null)
                {
                    tagIdArray[i] = tag.tagID;
                }
            }
            for (int i = 0; i < tagIdArray.Length; i++)
            {
                tempId = tagIdArray[i];
                List<StoriesTag> storyTags = smokeFreeDB.StoriesTag.Where(x => x.tagID == tempId).ToList();
                for(int j=0; j< storyTags.Count; j++)
                {
                    tempId = storyTags[j].storyID;
                    displayList.Add(smokeFreeDB.Story.Where(x => x.storyID == tempId).FirstOrDefault());
                }

            }
            return displayList;
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
                return RedirectToAction("SignInMember", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
            ViewBag.tagList = getTags(id);
            Story story = smokeFreeDB.Story.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }
        public List<String> getTags(int? id)
        {
            List<StoriesTag> storyTags = smokeFreeDB.StoriesTag.Where(x => x.storyID == id).ToList();
            List<String> displayList = new List<string>();
            Tag tagItem;
            int temp;
            for(int i = 0; i < storyTags.Count; i++)
            {
                temp = storyTags[i].tagID;
                tagItem = smokeFreeDB.Tag.Where(x => x.tagID == temp).FirstOrDefault();
                displayList.Add(tagItem.tagName);
            }
            return displayList;
        }

        public ActionResult CreateStory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStory([Bind(Include = "storyID,userName,title, body,postDate")] Models.Story story)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInMember", "Account");
            }
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

    }
}