using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SmokeFreeApplication.Models;

namespace SmokeFreeApplication.Controllers
{
    public class CommentsController : Controller
    {
        private SmokeFreeDBContext db = new SmokeFreeDBContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comment.ToList());
        }

        // GET: Comments/Create
        public ActionResult CreateComment(CommentQuery q)
        {
            Comment comment = new Comment();
            comment.parentID = q.id;
            comment.parentType = q.pType;
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CommentQuery c,[Bind(Include = "commentID,parentType,parentID,body,postDate,userName,status")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.parentType = c.pType;
                comment.parentID = c.id;

                comment.userName = Session["username"].ToString();
                comment.postDate = DateTime.Now;
                comment.status = "visible";

                
                if (comment.body != "" && comment.body != null)
                {
                    db.Comment.Add(comment);
                    db.SaveChanges();
                    db.Entry(comment).Reload();
                }

                return findParent(comment);
            }
            return View(comment);
        }

        public ActionResult findParent (Comment comment)
        {
            // Take the current comment being modified/created as a parameter to find the root story or article
            int currParent = 0;
            Comment curr = comment;
            
            while (curr.parentType == "C")
            {
                currParent = curr.parentID;
                curr = db.Comment.Where(x => x.commentID == currParent).FirstOrDefault();
            }
            
            // Depending on the root of the comment, redirect to appropriate location
            if (curr.parentType == "S")
            {
                Story story;
                story = db.Story.Find(curr.parentID);
                return Redirect("~/Story/ViewStory/" + story.storyID); 
            }
            else if (curr.parentType == "A")
            {
                Article article;
                article = db.Article.Find(curr.parentID);
                return Redirect("~/Article/ViewArticle/" + article.articleID); 
            }

            // Something went wrong, could not find root
            return Redirect("~/Home/");
        }
        public ActionResult ViewComment(CommentQuery q)
        {
            var comments = from c in db.Comment
                           where c.parentID == q.id
                           where c.parentType == q.pType
                           orderby c.postDate descending
                           select c;

            return View(comments.ToList());
            }


            // GET: Comments/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentID,parentType,parentID,body,postDate,userName,status")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            comment.body = null;
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            //db.Comment.Remove(comment);
            comment.status = "del"; // Update the status so that we don't display
            db.SaveChanges();
            return findParent(comment);

            //return View(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    // For easier passing of comment data
    // Allows display of comments by story/article/comments
    // Allows creation of comments by story/article/comments
    public class CommentQuery
    {
        public int id { get; set; }
        public string pType { get; set; }

        public CommentQuery(int i, string p)
        {
            id = i; 
            pType = p;
        }
        public CommentQuery()
        {
            id = 999;
            pType = "X";
        }
    }
}
