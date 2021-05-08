using OnlineFloralDelivery.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class HomeController : Controller
    {
        BouquetDAO bouquetDAO = new BouquetDAO();
        OccasionDAO occasionDAO = new OccasionDAO();
        SaleDAO saleDAO = new SaleDAO();
        public ActionResult Index()
        {
            ViewBag.One_Bouquet = bouquetDAO.GetNewBouquet().Take(1);
            ViewBag.Four_Bouquet = bouquetDAO.Get().Take(4);
            ViewBag.Eight_New_Product = bouquetDAO.GetBouquet().Take(8);
            ViewBag.Five_Category_For_Filter = occasionDAO.Get().Take(5);
            ViewBag.Sale = saleDAO.GetB().Take(3);
            ViewBag.Sale_Bouquet = bouquetDAO.GetSaleBouquet().Take(3);
            ViewBag.Sale_BouquetEx = bouquetDAO.GetBouquetExpensive().Take(3);
            ViewBag.Sale_BouquetCh = bouquetDAO.GetBouquetCheap().Take(3);
            ViewBag.Discount = saleDAO.Get().Take(1);
            return View();
        }

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            ViewBag.Occasion_List = occasionDAO.Get();
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            @TempData["ContactActive"] = "active";
            return View();
        }
    }
}