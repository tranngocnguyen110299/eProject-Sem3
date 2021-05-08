using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class OrderDAO
    {
        OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderID;
        }
    }
}