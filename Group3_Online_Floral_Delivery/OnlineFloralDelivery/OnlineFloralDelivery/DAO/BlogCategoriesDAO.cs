using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class BlogCategoriesDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private string ftpChild = "imgBouquets";

        public List<BlogCategoriesViewModel> getAllBlogCategories()
        {
            var blogCate = (from blogCa in db.BlogCategories
                            select new BlogCategoriesViewModel()
                            {
                                BlogCategoriesID = blogCa.BlogCategoriesID,
                                BlogCategoriesName = blogCa.BlogCategoriesName
                            }).ToList();
            return blogCate;
        }

        public List<BouquetDetailViewModel> geAllImagesOfProduct(int? id)
        {
            var BouImage = (from pro in db.Bouquets
                                from ProImg in pro.BouquetImages
                                where pro.Quantity > 0 && pro.BouquetID == id
                                select new BouquetDetailViewModel()
                                {
                                    ImageID = ProImg.ImageID,
                                    ImageFileName = ProImg.ImageFileName
                                }).ToList();
            foreach (var item in BouImage)
            {
                item.ImageFileName = new ImageProvider().LoadImage(item.ImageFileName, ftpChild);
            }
            return BouImage;
        }
    }
}