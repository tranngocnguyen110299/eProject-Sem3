using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class BouquetViewModel
    {
        public int BouquetID { get; set; }
        public string BouquetName { get; set; }
        public int OccasionID { get; set; }
        public string OccasionName { get; set; }
        public int SaleID { get; set; }
        public string SaleName { get; set; }
        public string MainImage { get; set; }
        public int UnitPrice { get; set; }
        public int OldUnitPrice { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
    }
}