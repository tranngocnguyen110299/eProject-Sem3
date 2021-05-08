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
    public class PaymentsController : BaseController
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgPayments";

        // GET: Admin/Payments
        public ActionResult Index()
        {
            var list = db.Payments.ToList();
            foreach (var item in list)
            {
                item.Picture = imgProvider.LoadImage(item.Picture, ftpChild);
            }
            return View(list);
        }

        // GET: Admin/Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            payment.Picture = imgProvider.LoadImage(payment.Picture, ftpChild);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Admin/Payments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,PaymentName,ImageFile")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(payment.ImageFile.FileName);
                string extension = Path.GetExtension(payment.ImageFile.FileName);
                if (imgProvider.Validate(payment.ImageFile) != null)
                {
                    ViewBag.Error = imgProvider.Validate(payment.ImageFile);
                    return View(payment);
                }
                payment.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                db.Payments.Add(payment);
                db.SaveChanges();
                SaveImage(payment.ImageFile, payment.Picture, ftpChild);
                TempData["Notice_Create_Success"] = true;
                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Admin/Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            Session["OldImage"] = payment.Picture;
            return View(payment);
        }

        // POST: Admin/Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,PaymentName,ImageFile")] Payment payment, string imageOldFile)
        {
            if (ModelState.IsValid)
            {
                if (payment.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(payment.ImageFile.FileName);
                    string extension = Path.GetExtension(payment.ImageFile.FileName);

                    if (imgProvider.Validate(payment.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(payment.ImageFile);
                        return View("Edit");
                    }
                    payment.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    DeleteImage(imgProvider.LoadImage(imageOldFile, ftpChild));
                    SaveImage(payment.ImageFile, payment.Picture, ftpChild);
                }
                else
                {
                    payment.Picture = imageOldFile;
                }
                db.Entry(payment).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Session.Remove("OldImage");
                    TempData["Notice_Save_Success"] = true;
                }
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Admin/Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            payment.Picture = imgProvider.LoadImage(payment.Picture, ftpChild);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Admin/Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Payment payment = db.Payments.Find(id);
                db.Payments.Remove(payment);
                db.SaveChanges();
                DeleteImage(imgProvider.LoadImage(payment.Picture, ftpChild));
                TempData["Notice_Delete_Success"] = true;
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
