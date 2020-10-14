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
        public ActionResult CreateComment(CommentQuery c)
        {
            Comment comment = new Comment();
            comment.parentID = c.id;
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CommentQuery c, [Bind(Include = "commentID,parentType,parentID,body,postDate,userName")] Comment comment)
        {
            
            if (ModelState.IsValid)
            {
                comment.postDate = DateTime.Now;
                comment.parentType = c.pType;
                comment.parentID = c.id;
                comment.userName = Session["username"].ToString();
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ViewStory/"+c.id.ToString(), "Story");
            }

            return View(comment);
        }
        public ActionResult ViewComment(CommentQuery q)
        {
            var comments = from c in db.Comment
                           where c.parentID == q.id
                           where c.parentType == q.pType
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
        public ActionResult Edit([Bind(Include = "commentID,parentType,parentID,body,postDate,userName")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            db.Comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
