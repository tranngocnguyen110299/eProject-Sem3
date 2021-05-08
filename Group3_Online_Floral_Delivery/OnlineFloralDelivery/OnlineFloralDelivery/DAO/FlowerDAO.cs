using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class FlowerDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgFlowers";


        public List<FlowerViewModel> GetFlower()
        {
            var list = (from flo in db.Flowers
                        where flo.Status == true
                        select new FlowerViewModel()
                        {
                            FlowerID = flo.FlowerID,
                            FlowerName = flo.FlowerName,
                            Picture = flo.Picture
                        }).ToList();

            foreach (var item in list)
            {
                item.Picture = new ImageProvider().LoadImage(item.Picture, ftpChild);
            }

            return list;
        }

        public List<FlowerViewModel> GetFlowerMeaning(int flowerID)
        {
            var list = (from flo in db.Flowers
                        where flo.Status == true && flo.FlowerID == flowerID
                        select new FlowerViewModel()
                        {
                            FlowerID = flo.FlowerID,
                            FlowerName = flo.FlowerName,
                            Meaning = flo.Meaning,
                            Picture = flo.Picture
                        }).ToList();

            foreach (var item in list)
            {
                item.Picture = new ImageProvider().LoadImage(item.Picture, ftpChild);
            }

            return list;
        }

    }
}