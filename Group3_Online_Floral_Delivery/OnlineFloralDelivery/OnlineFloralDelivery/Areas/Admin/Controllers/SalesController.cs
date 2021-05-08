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
using Rotativa;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0")]
    public class SalesController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgSales";

        // GET: Admin/Sales
        public ActionResult Index()
        {
            var list = db.Sales.ToList();
            foreach (var item in list)
            {
                item.Picture = imgProvider.LoadImage(item.Picture, ftpChild);
            }
            return View(list);
        }

        // GET: Admin/Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            sale.Picture = imgProvider.LoadImage(sale.Picture, ftpChild);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Admin/Sales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,SaleName,Content,StartDate,EndDate,Picture,Discount,ImageFile")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                if (sale.StartDate > sale.EndDate)
                {
                    ViewBag.NotiDate = "The start date must be before the end date.";
                    return View(sale);
                }
                string fileName = Path.GetFileNameWithoutExtension(sale.ImageFile.FileName);
                string extension = Path.GetExtension(sale.ImageFile.FileName);

                if (imgProvider.Validate(sale.ImageFile) != null)
                {
                    ViewBag.Error = imgProvider.Validate(sale.ImageFile);
                    return View(sale);
                }

                sale.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                db.Sales.Add(sale);
                if (db.SaveChanges() > 0)
                {
                    SaveImage(sale.ImageFile, sale.Picture, ftpChild);
                    TempData["Notice_Create_Success"] = true;
                }
                return RedirectToAction("Index");
            }

            return View(sale);
        }

        // GET: Admin/Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            Session["OldImage"] = sale.Picture;
            return View(sale);
        }

        // POST: Admin/Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,SaleName,Content,StartDate,EndDate,Picture,Discount,ImageFile")] Sale sale, string imageOldFile)
        {
            if (ModelState.IsValid)
            {
                if (sale.StartDate > sale.EndDate)
                {
                    ViewBag.NotiDate = "The start date must be before the end date.";
                    return View("Edit");
                }

                if (sale.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sale.ImageFile.FileName);
                    string extension = Path.GetExtension(sale.ImageFile.FileName);

                    if (imgProvider.Validate(sale.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(sale.ImageFile);
                        return View("Edit");
                    }
                    sale.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    DeleteImage(imgProvider.LoadImage(imageOldFile, ftpChild));
                    SaveImage(sale.ImageFile, sale.Picture, ftpChild);
                }
                else
                {
                    sale.Picture = imageOldFile;
                }
                db.Entry(sale).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Session.Remove("OldImage");
                    TempData["Notice_Save_Success"] = true;
                }
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        // GET: Admin/Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            sale.Picture = imgProvider.LoadImage(sale.Picture, ftpChild);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Admin/Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Sale sale = db.Sales.Find(id);
                db.Sales.Remove(sale);
                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(sale.Picture, ftpChild));
                    TempData["Notice_Delete_Success"] = true;
                }
            }
            catch (Exception)
            {
                TempData["Notice_Delete_Fail"] = true;
            }
            return RedirectToAction("Index");
        }
        public ActionResult PrintAll()
        {
            var q = new ActionAsPdf("Index");
            return q;
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
