using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class BouquetDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgMainBouquets";


        public List<BouquetViewModel> GetBouquet()
        {
            var list = (from bou in db.Bouquets
                        join occ in db.Occasions on bou.OccasionID equals occ.OccasionID
                        where bou.Quantity > 0 && bou.Status == false
                        select new BouquetViewModel()
                        {
                            BouquetID = bou.BouquetID,
                            BouquetName = bou.BouquetName,
                            UnitPrice = bou.UnitPrice,
                            OldUnitPrice = bou.OldUnitPrice,
                            OccasionID = bou.OccasionID,
                            OccasionName = occ.OccasionName,
                            MainImage = bou.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }

            return list;
        }

        public List<BouquetViewModel> GetBouquetExpensive()
        {
            var list = (from bou in db.Bouquets
                        join occ in db.Occasions on bou.OccasionID equals occ.OccasionID
                        where bou.Quantity > 0 && bou.Status == false
                        orderby bou.UnitPrice descending
                        select new BouquetViewModel()
                        {
                            BouquetID = bou.BouquetID,
                            BouquetName = bou.BouquetName,
                            UnitPrice = bou.UnitPrice,
                            OldUnitPrice = bou.OldUnitPrice,
                            OccasionID = bou.OccasionID,
                            OccasionName = occ.OccasionName,
                            MainImage = bou.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }

            return list;
        }

        public List<BouquetViewModel> GetBouquetCheap()
        {
            var list = (from bou in db.Bouquets
                        join occ in db.Occasions on bou.OccasionID equals occ.OccasionID
                        where bou.Quantity > 0 && bou.Status == false
                        orderby bou.UnitPrice ascending
                        select new BouquetViewModel()
                        {
                            BouquetID = bou.BouquetID,
                            BouquetName = bou.BouquetName,
                            UnitPrice = bou.UnitPrice,
                            OldUnitPrice = bou.OldUnitPrice,
                            OccasionID = bou.OccasionID,
                            OccasionName = occ.OccasionName,
                            MainImage = bou.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }

            return list;
        }

        public List<BouquetViewModel> GetSaleBouquet()
        {
            var list = (from bou in db.Bouquets
                        join sale in db.Sales on bou.SaleID equals sale.SaleID
                        join occ in db.Occasions on bou.OccasionID equals occ.OccasionID
                        where bou.Quantity > 0 && sale.SaleName != "No promotion"
                        orderby sale.SaleID descending
                        select new BouquetViewModel()
                        {
                            BouquetID = bou.BouquetID,
                            BouquetName = bou.BouquetName,
                            UnitPrice = bou.UnitPrice,
                            OldUnitPrice = bou.OldUnitPrice,
                            SaleID = sale.SaleID,
                            SaleName = sale.SaleName,
                            OccasionID = occ.OccasionID,
                            OccasionName = occ.OccasionName,
                            MainImage = bou.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }
            return list;
        }



        public List<BouquetViewModel> GetNewBouquet()
        {
            var list = (from pro in db.Bouquets
                        where pro.Quantity > 0 && pro.Status == false
                        orderby pro.BouquetID descending
                        select new BouquetViewModel()
                        {
                            BouquetID = pro.BouquetID,
                            BouquetName = pro.BouquetName,
                            UnitPrice = pro.UnitPrice,
                            OldUnitPrice = pro.OldUnitPrice,
                            OccasionID = pro.OccasionID,
                            ShortDescription = pro.ShortDescription,
                            MainImage = pro.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }

            return list;
        }

        public List<BouquetViewModel> Get()
        {
            var list = (from pro in db.Bouquets
                        where pro.Quantity > 0 && pro.Status == false
                        select new BouquetViewModel()
                        {
                            BouquetID = pro.BouquetID,
                            BouquetName = pro.BouquetName,
                            UnitPrice = pro.UnitPrice,
                            OldUnitPrice = pro.OldUnitPrice,
                            OccasionID = pro.OccasionID,
                            MainImage = pro.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }

            return list;
        }

        public List<BouquetViewModel> GetOccasion(int id)
        {
            var list = (from pro in db.Bouquets
                        join cate in db.Occasions on pro.OccasionID equals cate.OccasionID
                        where pro.Quantity > 0 && pro.Status == false && pro.OccasionID == id
                        select new BouquetViewModel()
                        {
                            BouquetID = pro.BouquetID,
                            BouquetName = pro.BouquetName,
                            UnitPrice = pro.UnitPrice,
                            OldUnitPrice = pro.OldUnitPrice,
                            OccasionID = pro.OccasionID,
                            MainImage = pro.MainImage
                        }).ToList();

            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }

            return list;
        }


        public Bouquet ViewDetail(int id)   
        {
            var bouquet = db.Bouquets.SingleOrDefault(p => p.BouquetID == id);

            bouquet.MainImage = new ImageProvider().LoadImage(bouquet.MainImage, ftpChild);

            return bouquet;
        }

        public List<BouquetViewModel> Search(string key)
        {
            var list = (from pro in db.Bouquets
                        join cate in db.Occasions on pro.OccasionID equals cate.OccasionID
                        join sale in db.Sales on pro.SaleID equals sale.SaleID
                        where pro.Quantity > 0 && pro.BouquetName.Contains(key)
                        select new BouquetViewModel
                        {
                            BouquetID = pro.BouquetID,
                            BouquetName = pro.BouquetName,
                            UnitPrice = pro.UnitPrice,
                            OldUnitPrice = pro.OldUnitPrice,
                            SaleID = sale.SaleID,
                            SaleName = sale.SaleName,
                            OccasionID = cate.OccasionID,
                            OccasionName = cate.OccasionName,
                            MainImage = pro.MainImage
                        }).ToList();
            foreach (var item in list)
            {
                item.MainImage = new ImageProvider().LoadImage(item.MainImage, ftpChild);
            }
            return list;
        }
    }
}