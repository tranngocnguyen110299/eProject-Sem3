using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class PaymentDAO
    {
        OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        public List<Payment> GetAll()
        {
            return db.Payments.ToList();
        }
    }
}