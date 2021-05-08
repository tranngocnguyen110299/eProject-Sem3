using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0,1")]
    public class BlogsController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgBlogs";

        // GET: Admin/Blogs
        public ActionResult Index()
        {
            var list = db.Blogs.Include(b => b.BlogCategory).Include(b => b.Employee).ToList();
            foreach (var item in list)
            {
                item.Picture = imgProvider.LoadImage(item.Picture, ftpChild);
            }
            return View(list);
        }

        // GET: Admin/Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            blog.Picture = imgProvider.LoadImage(blog.Picture, ftpChild);
            BlogCategory blogCategories = db.BlogCategories.Find(blog.BlogCategoriesID);
            ViewBag.BlogCategory = blogCategories.BlogCategoriesName;
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Admin/Blogs/Create
        public ActionResult Create()
        {
            ViewBag.BlogCategoriesID = new SelectList(db.BlogCategories, "BlogCategoriesID", "BlogCategoriesName");
            ViewBag.Username = new SelectList(db.Employees, "Username", "Password");
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "BlogID,BlogName,Username,BlogCategoriesID,Content,Date,Picture,ImageFile")] Blog blogs)
        {
            if (ModelState.IsValid)
            {
                var check = db.Employees.SingleOrDefault(u => u.Username == blogs.Username);
                if (check != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(blogs.ImageFile.FileName);
                    string extension = Path.GetExtension(blogs.ImageFile.FileName);

                    if (imgProvider.Validate(blogs.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(blogs.ImageFile);
                        return View(blogs);
                    }
                    blogs.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    var user = (OnlineFloralDelivery.Areas.Admin.Models.UserLogin)Session[Common.CommonConstants.USER_SESSION];
                    blogs.Username = user.Username;
                    db.Blogs.Add(blogs);
                    if (db.SaveChanges() > 0)
                    {
                        SaveImage(blogs.ImageFile, blogs.Picture, ftpChild);
                        TempData["Notice_Create_Success"] = true;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.NotifyUser = "User does not exist, please try again.";
                    ViewBag.BlogCategoriesID = new SelectList(db.BlogCategories, "BlogCategoriesID", "BlogCategoriesName", blogs.BlogCategoriesID);
                    ViewBag.Username = new SelectList(db.Employees, "Username", "Password", blogs.Username);
                    return View(blogs);
                }
            }
            ViewBag.BlogCategoriesID = new SelectList(db.BlogCategories, "BlogCategoriesID", "BlogCategoriesName", blogs.BlogCategoriesID);
            ViewBag.Username = new SelectList(db.Employees, "Username", "Password", blogs.Username);
            return View(blogs);



        }

        // GET: Admin/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            Session["OldImage_User"] = blog.Picture;
            ViewBag.BlogCategoriesID = new SelectList(db.BlogCategories, "BlogCategoriesID", "BlogCategoriesName", blog.BlogCategoriesID);
            ViewBag.Username = new SelectList(db.Employees, "Username", "Password", blog.Username);
            return View(blog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "BlogID,BlogName,Username,BlogCategoriesID,Content,Date,Picture,ImageFile")] Blog blogs, String imageOldFile)
        {

            if (ModelState.IsValid)
            {
                if (blogs.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(blogs.ImageFile.FileName);
                    string extension = Path.GetExtension(blogs.ImageFile.FileName);

                    if (imgProvider.Validate(blogs.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(blogs.ImageFile);
                        return View("Edit");
                    }
                    blogs.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    DeleteImage(imgProvider.LoadImage(imageOldFile, ftpChild));
                    SaveImage(blogs.ImageFile, blogs.Picture, ftpChild);
                }
                else
                {
                    blogs.Picture = imageOldFile;
                }

                var user = (OnlineFloralDelivery.Areas.Admin.Models.UserLogin)Session[Common.CommonConstants.USER_SESSION];
                blogs.Username = user.Username;
                db.Entry(blogs).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Session.Remove("OldImage_User");
                    TempData["Notice_Save_Success"] = true;
                }
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoriesID = new SelectList(db.BlogCategories, "BlogCategoriesID", "BlogCategoriesName", blogs.BlogCategoriesID);
            ViewBag.Username = new SelectList(db.Employees, "Username", "Password", blogs.Username);
            return View(blogs);
        }

        // GET: Admin/Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blogs = db.Blogs.Find(id);
            blogs.Picture = imgProvider.LoadImage(blogs.Picture, ftpChild);
            BlogCategory blogCategories = db.BlogCategories.Find(blogs.BlogCategoriesID);
            ViewBag.BlogCategory = blogCategories.BlogCategoriesName;
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Blog blogs = db.Blogs.Find(id);
                db.Blogs.Remove(blogs);
                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(blogs.Picture, ftpChild));
                    TempData["Notice_Delete_Success"] = true;
                }
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

        public void SaveImage(HttpPostedFileBase imageFile, string fileName, string childNode)
        {
            string uploadFolderPath = Server.MapPath("~/public/uploadedFiles/" + childNode + "/");

            if (Directory.Exists(uploadFolderPath) == false)
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            fileName = Path.Combine(uploadFolderPath, fileName);

            imageFile.SaveAs(fileName);
        }

        public void DeleteImage(string path)
        {
            try
            {
                System.IO.File.Delete(Server.MapPath(path));
            }
            catch (Exception) { }

        }
    }
}
