using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.Models
{
    [Serializable]
    public class CartItem
    {
        public Bouquet Bouquet { get; set; }
        public int Quantity { get; set; }

    }
}