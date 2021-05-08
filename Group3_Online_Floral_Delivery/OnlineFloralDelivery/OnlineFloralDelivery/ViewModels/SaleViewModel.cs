using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class SaleViewModel
    {
        public int SaleID { get; set; }
        public int BouquetID { get; set; }
        public string BouquetName { get; set; }
        public string SaleName { get; set; }
        public string Content { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Picture { get; set; }
        public decimal Discount { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}