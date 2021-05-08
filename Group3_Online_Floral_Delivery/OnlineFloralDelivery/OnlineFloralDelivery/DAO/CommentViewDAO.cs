using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class CommentViewDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

        public bool Insert(Comment feedback)
        {
            feedback.CommentDate = DateTime.Now;
            db.Comments.Add(feedback);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}