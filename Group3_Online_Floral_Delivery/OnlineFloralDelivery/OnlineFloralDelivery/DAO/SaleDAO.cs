using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class SaleDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgSales";

        public List<SaleViewModel> Get()
        {
            var list = (from sale in db.Sales
                        join cate in db.Bouquets on sale.SaleID equals cate.SaleID
                        orderby sale.SaleID descending
                        where sale.SaleName != "No promotion"
                        select new SaleViewModel
                        {
                            SaleID = sale.SaleID,
                            SaleName = sale.SaleName,
                            BouquetID = cate.BouquetID,
                            Content = sale.Content,
                            StartDate = sale.StartDate,
                            EndDate = sale.EndDate,
                            Discount = sale.Discount,
                            Picture = sale.Picture
                        }).ToList();
            foreach (var item in list)
            {
                item.Picture = new ImageProvider().LoadImage(item.Picture, ftpChild);
            }
            return list;
        }

        public List<SaleViewModel> GetB()
        {
            var list = (from sale in db.Sales
                        join cate in db.Bouquets on sale.SaleID equals cate.SaleID
                        orderby sale.SaleID descending
                        where sale.SaleName != "No promotion"
                        select new SaleViewModel
                        {
                            SaleID = sale.SaleID,
                            SaleName = sale.SaleName,
                            BouquetID = cate.BouquetID,
                            BouquetName = cate.BouquetName,
                            Content = sale.Content,
                            StartDate = sale.StartDate,
                            EndDate = sale.EndDate,
                            Discount = sale.Discount,
                            Picture = sale.Picture
                        }).Distinct().ToList();
            foreach (var item in list)
            {
                item.Picture = new ImageProvider().LoadImage(item.Picture, ftpChild);
            }
            return list;
        }

    }
}