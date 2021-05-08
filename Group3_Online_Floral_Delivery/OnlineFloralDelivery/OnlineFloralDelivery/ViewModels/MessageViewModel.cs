using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.ViewModels
{
    public class MessageViewModel
    {
        public int OccasionID { get; set; }
        public string OccasionName { get; set; }

        public string MessageContent { get; set; }
        public int MessageID { get; internal set; }
    }
}