using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class OrderDetailDAO
    {
        OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

        public bool Insert(OrderDetail detail)
        {
            db.OrderDetails.Add(detail);
            if (db.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}