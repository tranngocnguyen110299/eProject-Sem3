using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFloralDelivery.Models;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0,1")]
    public class BlogCommentsController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

        // GET: Admin/BlogComments
        public ActionResult Index()
        {
            return View(db.BlogComments.ToList());
        }

        // GET: Admin/BlogComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComments = db.BlogComments.Find(id);
            Blog blogs = db.Blogs.Find(blogComments.BlogID);
            ViewBag.Blog = blogs.BlogName;
            if (blogComments == null)
            {
                return HttpNotFound();
            }
            return View(blogComments);
        }

        // GET: Admin/BlogComments/Create
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComments = db.BlogComments.Find(id);
            Blog blogs = db.Blogs.Find(blogComments.BlogID);
            ViewBag.Blog = blogs.BlogName;
            if (blogComments == null)
            {
                return HttpNotFound();
            }
            return View(blogComments);
        }

        // POST: Admin/BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                BlogComment blogComments = db.BlogComments.Find(id);
                db.BlogComments.Remove(blogComments);
                db.SaveChanges();
                TempData["Notice_Delete_Success"] = true;
            }
            catch (Exception)
            {
                TempData["Notice_Delete_Fail"] = true;
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
