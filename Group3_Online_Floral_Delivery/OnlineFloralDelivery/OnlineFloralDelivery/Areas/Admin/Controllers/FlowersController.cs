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
    public class FlowersController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgFlowers";

        // GET: Admin/Flowers
        public ActionResult Index()
        {
            var list = db.Flowers.ToList();
            foreach (var item in list)
            {
                item.Picture = imgProvider.LoadImage(item.Picture, ftpChild);
            }
            return View(list);
        }

        // GET: Admin/Flowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = db.Flowers.Find(id);
            flower.Picture = imgProvider.LoadImage(flower.Picture, ftpChild);
            Supplier sup = db.Suppliers.Find(flower.SupplierID);
            ViewBag.SupplierName = sup.SupplierName;
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }

        // GET: Admin/Flowers/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            return View();
        }

        // POST: Admin/Flowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "FlowerID,FlowerName,SupplierID,ShortDescription,UnitPrice,Quantity,Picture,Meaning,Status,ImageFile")] Flower flower)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(flower.ImageFile.FileName);
                string extension = Path.GetExtension(flower.ImageFile.FileName);
                if (imgProvider.Validate(flower.ImageFile) != null)
                {
                    ViewBag.Error = imgProvider.Validate(flower.ImageFile);
                    return View(flower);
                }
                flower.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;


                db.Flowers.Add(flower);
                if (db.SaveChanges() > 0)
                {
                    SaveImage(flower.ImageFile, flower.Picture, ftpChild);
                    TempData["Notice_Create_Success"] = true;
                }
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", flower.SupplierID);
            return View(flower);
        }

        // GET: Admin/Flowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return HttpNotFound();
            }
            Session["OldImage_User"] = flower.Picture;
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", flower.SupplierID);
            return View(flower);
        }

        // POST: Admin/Flowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "FlowerID,FlowerName,SupplierID,ShortDescription,UnitPrice,Quantity,Picture,Meaning,Status,ImageFile")] Flower flower, String imageOldFile_User)
        {
            if (ModelState.IsValid)
            {
                if (flower.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(flower.ImageFile.FileName);
                    string extension = Path.GetExtension(flower.ImageFile.FileName);

                    if (imgProvider.Validate(flower.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(flower.ImageFile);
                        return View("Edit");
                    }
                    flower.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    DeleteImage(imgProvider.LoadImage(imageOldFile_User, ftpChild));
                    SaveImage(flower.ImageFile, flower.Picture, ftpChild);
                }
                else
                {
                    flower.Picture = imageOldFile_User;
                }
                db.Entry(flower).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Session.Remove("OldImage_User");
                    TempData["Notice_Save_Success"] = true;
                }
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", flower.SupplierID);
            return View(flower);
        }

        // GET: Admin/Flowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = db.Flowers.Find(id);
            flower.Picture = imgProvider.LoadImage(flower.Picture, ftpChild);
            Supplier sup = db.Suppliers.Find(flower.SupplierID);
            ViewBag.SupplierName = sup.SupplierName;
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }

        // POST: Admin/Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Flower flower = db.Flowers.Find(id);
                db.Flowers.Remove(flower);
                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(flower.Picture, ftpChild));
                    TempData["Notice_Delete_Success"] = true;
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                TempData["Notice_Delete_Fail"] = true;
            }
            catch (Exception)
            {
                TempData["Notice_Delete_Fail"] = true;
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Import(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var flower = db.Flowers.FirstOrDefault(p => p.FlowerID == id);
            ViewBag.Flower = flower;
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Import([Bind(Include = "FlowerID,FlowerName,SupplierID,ShortDescription,UnitPrice,Quantity,Picture,Meaning,Status")] Flower flower)
        {
            if (ModelState.IsValid)
            {
                var flo = db.Flowers.Single(p => p.FlowerID == flower.FlowerID);
                flo.Quantity += flower.Quantity;
                db.SaveChanges();
                TempData["Notice_Save_Success"] = true;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Import", new { id = flower.FlowerID });
        }

        public ActionResult Export(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var flower = db.Flowers.FirstOrDefault(p => p.FlowerID == id);
            ViewBag.Flower = flower;
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Export([Bind(Include = "FlowerID,FlowerName,SupplierID,ShortDescription,UnitPrice,Quantity,Picture,Meaning,Status")] Flower flower)
        {
            if (ModelState.IsValid)
            {
                var flo = db.Flowers.Single(p => p.FlowerID == flower.FlowerID);
                flo.Quantity -= flower.Quantity;
                db.SaveChanges();
                TempData["Notice_Save_Success"] = true;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Export", new { id = flower.FlowerID });
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
