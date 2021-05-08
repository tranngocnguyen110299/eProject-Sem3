using OnlineFloralDelivery.DAO;
using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class BouquetController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        BouquetDAO bouquetDAO = new BouquetDAO();
        // GET: Bouquet
        public ActionResult Index(int id)
        {
            TempData["OccasionActive"] = "active";
            ViewBag.Occ = bouquetDAO.GetOccasion(id);
            ViewBag.Occc = db.Occasions.Find(id).OccasionName.ToString();
            return View();
        }
    }
}