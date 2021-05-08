using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class BouquetDetailViewModel
    {
        public int BouquetID { get; set; }
        public string BouqueName { get; set; }

        public string ImageFileName { get; set; }
        public int ImageID { get; internal set; }
    }
}