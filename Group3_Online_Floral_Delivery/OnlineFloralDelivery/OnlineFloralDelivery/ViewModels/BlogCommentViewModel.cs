using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class BlogCommentViewModel
    {
        public int BlogCommentID { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> BlogID { get; set; }
        
    }
}