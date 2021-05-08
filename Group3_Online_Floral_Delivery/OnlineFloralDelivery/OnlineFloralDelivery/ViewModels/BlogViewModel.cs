using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class BlogViewModel
    {
        public int BlogID { get; set; }
        public string BlogName { get; set; }
        public string Username { get; set; }
        public int BlogCategoriesID { get; set; }
        public string Content { get; set; }
        public System.DateTime Date { get; set; }
        public string Picture { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
        public virtual Employee Employee { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}