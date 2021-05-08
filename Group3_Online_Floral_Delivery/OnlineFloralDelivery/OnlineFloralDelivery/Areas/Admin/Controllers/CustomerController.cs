using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0")]
    public class CustomerController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgCustomers";
        public ActionResult Index()
        {
            var list = db.Customers.ToList();
            foreach (var item in list)
            {
                item.Picture = imgProvider.LoadImage(item.Picture, ftpChild);
            }
            return View(list);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cus = db.Customers.Find(id);
            cus.Picture = imgProvider.LoadImage(cus.Picture, ftpChild);
            if (cus == null)
            {
                return HttpNotFound();
            }
            return View(cus);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cus = db.Customers.Find(id);
            if (cus == null)
            {
                return HttpNotFound();
            }
            Session["OldImage_User"] = cus.Picture;
            return View(cus);
        }

        [HttpPost]
        public ActionResult Edit(string Username, bool status)
        {
            if (ModelState.IsValid)
            {
                var emp = db.Customers.Select(p => p).Where(p => p.Username == Username).FirstOrDefault();
                emp.Status = status;
                if (db.SaveChanges() > 0)
                {
                    TempData["Notice_Save_Success"] = true;
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }
}