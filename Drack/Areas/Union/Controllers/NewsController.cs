using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Drack.Models;
using Drack.Extensions;

namespace Drack.Areas.Union.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private DrackContext db = new DrackContext();

        // GET: Admin/News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,Headline,NewsDescription,NewsImagePath,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] News news, HttpPostedFileBase ProfileImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ProfileImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(ProfileImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/news"), file);

                    //file upload
                    ProfileImageFile.SaveAs(path);
                    news.NewsImagePath = file;
                }
                var userId = User.Identity.GetId();
                news.CreatedDate = DateTime.Now;
                news.CreatedBy = userId;
                news.ModifiedDate = DateTime.Now;
                news.ModifiedBy = userId;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,Headline,NewsDescription,NewsImagePath,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] News news, HttpPostedFileBase ProfileImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ProfileImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(ProfileImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/news"), file);

                    //file upload
                    ProfileImageFile.SaveAs(path);
                    //updating imagepath
                    string fullPath = Request.MapPath("~/Repository/images/news/" + news.NewsImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    news.NewsImagePath = file;
                }
                var userId = User.Identity.GetId();
                news.ModifiedDate = DateTime.Now;
                news.ModifiedBy = userId;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            string fullPath = Request.MapPath("~/Repository/images/news/" + news.NewsImagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
}
