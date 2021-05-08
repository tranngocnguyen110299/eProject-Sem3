using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class CommentViewModel
    {
        public int CommentID { get; set; }
        public int BouquetID { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public System.DateTime CommentDate { get; set; }

        public virtual Bouquet Bouquet { get; set; }
        public virtual Customer Customer { get; set; }
    }
}