using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    [CategoryFilter]
    public class ArticlesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Articles
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [Authorize(Roles = "Owners")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")]
        public ActionResult Create([Bind(Include = "Id,Title,Body,CategoryName")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.CreatedOn = DateTime.Now;
                article.Modified = DateTime.Now;

                var category = db.Categories
                    .Where(x => x.CategoryName.Equals(article.CategoryName))
                    .FirstOrDefault();

                if (category == null)
                {
                    category = new Category()
                    {
                        CategoryName = article.CategoryName,
                        Count = 1
                    };
                    db.Categories.Add(category);
                }
                 else
                 {
                    category.Count++;
                    db.Entry(category).State = EntityState.Modified;
                 }

                article.Category = category;

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Owners")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            article.CategoryName = article.Category.CategoryName;

            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,CategoryName")] Article article)
        {
            if (ModelState.IsValid)
            {
                var dbArticle = db.Articles.Find(article.Id);
                if (dbArticle == null)
                {
                    return HttpNotFound();
                }

                dbArticle.Title = article.Title;
                dbArticle.Body = article.Body;
                dbArticle.Modified = DateTime.Now;
                dbArticle.CreatedOn = DateTime.Now;
                dbArticle.CategoryName = article.CategoryName;

                var beforeCategory = dbArticle.Category;
                if (!beforeCategory.CategoryName.Equals(article.CategoryName))
                {
                    beforeCategory.Articles.Remove(dbArticle);
                    beforeCategory.Count--;
                    db.Entry(beforeCategory).State = EntityState.Modified;

                    var category = db.Categories
                        .Where(item => item.CategoryName.Equals(article.CategoryName))
                        .FirstOrDefault();

                    if (category == null )
                    {
                        category = new Category()
                        {
                            CategoryName = article.CategoryName,
                            Count = 1
                        };
                        db.Categories.Add(category);
                    }
                    else
                    {
                        category.Count++;
                        db.Entry(category).State = EntityState.Modified;
                    }

                    dbArticle.Category = category;
                }

                db.Entry(dbArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Owners")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);

            Category category = article.Category;
            if (category != null)
            {
                category.Count--;
                db.Entry(category).State = EntityState.Modified;
            }

            article.Comments.Clear();

            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CreateComment([Bind(Include = "ArticleId,Body")] Comment comment)
        {
            var article = db.Articles.Find(comment.ArticleId);
            if (article == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            comment.Created = DateTime.Now;
            comment.Article = article;

            db.Comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = comment.ArticleId });
        }

        [Authorize(Roles = "Owners")]
        public ActionResult DeleteComment (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            var comment = db.Comments.Find(id);
            int articleId = comment.Article.Id;

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = articleId });

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
}
