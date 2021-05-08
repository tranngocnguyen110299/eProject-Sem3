using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class BouquetDetailDAO

    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private string ftpChild = "imgBouquets";
        public List<BouquetDetailViewModel> geAllImagesOfBouquet(int? id)
        {
            var bouquetImage = (from bou in db.Bouquets
                                from BouImg in bou.BouquetImages
                                where bou.Quantity > 0 && bou.BouquetID == id
                                select new BouquetDetailViewModel()
                                {
                                    ImageID = BouImg.ImageID,
                                    ImageFileName = BouImg.ImageFileName
                                }).ToList();
            foreach (var item in bouquetImage)
            {
                item.ImageFileName = new ImageProvider().LoadImage(item.ImageFileName, ftpChild);
            }
            return bouquetImage;
        }

        public List<CommentViewModel> geAllFeedbackOfProduct(int? id)
        {
            var feedbackOfProduct = (from pro in db.Comments
                                     where pro.BouquetID == id
                                     orderby pro.CommentDate descending
                                     select new CommentViewModel()
                                     {
                                         CommentID = pro.CommentID,
                                         Username = pro.Username,
                                         Content = pro.Content,
                                         CommentDate = pro.CommentDate,
                                         BouquetID = pro.BouquetID
                                     }).ToList();
            return feedbackOfProduct;
        }

        public List<Quantity> getSL(int? id)
        {
            var bouquet = (from bd in db.BouquetDetails
                                join b in db.Bouquets on bd.BouquetID equals b.BouquetID
                                join f in db.Flowers on bd.FlowerID equals f.FlowerID
                                   where b.Quantity > 0 && b.BouquetID == id
                                select new Quantity()
                                {
                                    BouquetID = b.BouquetID,
                                    FlowerID = f.FlowerID,
                                    FlowerName = f.FlowerName,
                                    Quantityy = bd.Quantity
                                }).ToList();
            return bouquet;
        }

    }
}