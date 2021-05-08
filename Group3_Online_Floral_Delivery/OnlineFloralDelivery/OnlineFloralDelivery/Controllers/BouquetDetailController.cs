using OnlineFloralDelivery.DAO;
using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class BouquetDetailController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

        BouquetDAO bouquetDAO = new BouquetDAO();
        OccasionDAO occasionDAO = new OccasionDAO();
        SaleDAO saleDAO = new SaleDAO();
        BouquetDetailDAO bouquetDetailDAO = new BouquetDetailDAO();
        CommentViewDAO com = new CommentViewDAO();
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Bouquets.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Bouquet = db.Bouquets.Find(id);
                ViewBag.Image = bouquetDetailDAO.geAllImagesOfBouquet(id).Take(4);
                ViewBag.Feedback = bouquetDetailDAO.geAllFeedbackOfProduct(id);
                ViewBag.SL = bouquetDetailDAO.getSL(id);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment feedback)
        {
            if (ModelState.IsValid)
            {
                var feedbackDAO = new CommentViewDAO().Insert(feedback);
                if (feedbackDAO == true)
                {
                    return RedirectToAction("Index", "BouquetDetail", new { id = feedback.BouquetID });
                }
            }
            return RedirectToAction("Index", "BouquetDetail", new { id = feedback.BouquetID });
        }
    }
}