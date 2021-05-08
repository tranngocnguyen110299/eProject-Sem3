using OnlineFloralDelivery.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class FlowerController : Controller
    {
        FlowerDAO flowerDAO = new FlowerDAO();
        // GET: Flower
        public ActionResult Index()
        {
            TempData["Meaning"] = "active";
            ViewBag.Flo = flowerDAO.GetFlower();
            return View();
        }

        public ActionResult Meaning(int flowerID)
        {
            ViewBag.Flo = flowerDAO.GetFlowerMeaning(flowerID);
            return View();
        }
    }
}