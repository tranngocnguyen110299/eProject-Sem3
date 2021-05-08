using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0,1")]
    public class OrdersController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgCustomers";

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Payment);
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orders = db.Orders.Find(id);
            orders.Customer.Picture = imgProvider.LoadImage(orders.Customer.Picture, ftpChild);
            var _orderDetail = (from ord in db.OrderDetails
                                join or in db.Orders on ord.OrderID equals or.OrderID
                                join pro in db.Bouquets on ord.BouquetID equals pro.BouquetID
                                where ord.OrderID == orders.OrderID
                                select ord).ToList();
            ViewBag.OrderDetail = _orderDetail;
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        public ActionResult Ship(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var order = db.Orders.Find(id);
                order.Status = 1;
                order.ShippingDate = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.Customers, "Username", "Password");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Username,PaymentID,Message,CreationDate,Recipient,Address,Phone,ShippingDate,Note,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Customers, "Username", "Password", order.Username);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName", order.PaymentID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Customers, "Username", "Password", order.Username);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName", order.PaymentID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Username,PaymentID,Message,CreationDate,Recipient,Address,Phone,ShippingDate,Note,Status")] Order orders)
        {
            if (ModelState.IsValid)
            {
                var order = db.Orders.Find(orders.OrderID);
                order.Address = orders.Address;
                order.Note = orders.Note;
                order.PaymentID = orders.PaymentID;
                order.Status = orders.Status;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.Customers, "Username", "Password", orders.Username);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentName", orders.PaymentID);
            return View(orders);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
}
