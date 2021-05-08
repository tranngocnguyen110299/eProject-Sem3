using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class FlowerViewModel
    {
        public int FlowerID { get; set; }
        public string FlowerName { get; set; }
        public int SupplierID { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<int> UnitPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Picture { get; set; }
        public string Meaning { get; set; }
        public bool Status { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}