using OnlineFloralDelivery.Common;
using OnlineFloralDelivery.DAO;
using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
namespace OnlineFloralDelivery.Controllers
{
    public class CartController : Controller
    {
        OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int bouquetID, int quantity)
        {  
            var bouquet = new BouquetDAO().ViewDetail(bouquetID);
            var cart = Session[CommonConstants.CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Bouquet.BouquetID == bouquetID))
                {
                    foreach (var item in list)
                    {
                        if (item.Bouquet.BouquetID == bouquetID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Bouquet = bouquet;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Bouquet = bouquet;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CommonConstants.CartSession] = list;
            }
            return RedirectToAction("Index");

        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Bouquet.BouquetID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Bouquet.BouquetID == item.Bouquet.BouquetID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
                if (item.Quantity == 0)
                {
                    sessionCart.Remove(item);
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult Checkout()
        {
            if (Session[Common.CommonConstants.CLIENT_SESSION] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var client = (OnlineFloralDelivery.Models.UserLogin)Session["CLIENT_SESSION"];
            if (client.Role != -1)
            {
                TempData["PleaseLogout"] = true;
                return RedirectToAction("Index", "Home");
            }
            var cart = Session[CommonConstants.CartSession];

            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.Messages = new MessageDAO().GetAll();
            ViewBag.Payments = new PaymentDAO().GetAll();
            return View(list);
        }

        [HttpPost]
        public ActionResult Checkout(string username, string note, int? payment, string recipient, string phone, string messageID, string messageID2, string address)
        {
            var or = new Models.Order();
            if (recipient.ToString().Length == 0)
            {
                TempData["ErrorMess"] = "Please enter your recipient.";
                return RedirectToAction("Checkout");
            }
            
            if (messageID == null)
            {
                TempData["ErrorMess"] = "Please choose messages";
                return RedirectToAction("Checkout");
            }
            if (address.Trim() == "")
            {
                TempData["ErrorMess"] = "Please enter your address.";
                return RedirectToAction("Checkout");
            }
            String phonePattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Match match = Regex.Match(phone, phonePattern);
            if (match == null)
            {
                TempData["ErrorMess"] = "Please enter phone.";
                return RedirectToAction("Checkout");
            }
            if (phone.Trim().Length == 0 )
            {
                TempData["ErrorMess"] = "Please enter your phone.";
                return RedirectToAction("Checkout");
            }
            if (payment.ToString().Length == 0)
            {
                TempData["ErrorMess"] = "Please choose your form of payment!";
                return RedirectToAction("Checkout");
            }
            if (messageID2 != "")
            {
                messageID = messageID2;
            }
           
            var order = new Order();
            order.Username = username;
            order.Note = note;
            order.PaymentID = (int)payment;
            order.CreationDate = DateTime.Now;
            order.Message = messageID;
            order.Recipient = recipient;
            order.Address = address;
            order.Phone = phone;
            
            order.Status = 0;



            DateTime Ship = order.CreationDate;
            int hour = DateTime.Now.Hour;
            int minu = DateTime.Now.Minute;
            int hourEnd = 21;
            if (hour <= hourEnd)
            {
                if (hourEnd - hour < 5 || minu > 1 && hourEnd - hour == 5)
                {
                    TempData["ErrorMess"] = "Your item will be delivered the next morning!";
                    Ship = order.CreationDate.AddDays(1);
                }
            }
            else
            {
                TempData["ErrorMess"] = "Your item will be delivered the next morning!";
                Ship = order.CreationDate.AddDays(1);
            }
            order.ShippingDate = Ship;

            var orderDAO = new OrderDAO();
            var orderDetailDAO = new OrderDetailDAO();
            try
            {
                var id = orderDAO.Insert(order);
                var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = (int)id;
                    orderDetail.BouquetID = item.Bouquet.BouquetID;
                    orderDetail.UnitPrice = item.Bouquet.UnitPrice;
                    orderDetail.Quantity = item.Quantity;

                    orderDetailDAO.Insert(orderDetail);
                }
            }
            catch (Exception)
            {
                return Redirect("cart");
            }
            Session[CommonConstants.CartSession] = null;
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }



    }
}