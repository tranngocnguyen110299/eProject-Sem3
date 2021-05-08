using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class MessageDAO
    {
        OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        public List<Message> GetAll()
        {
            return db.Messages.ToList();
        }
        //public List<MessageViewModel> geAllMessageOfOccasion()
        //{
        //    var messages = (from bou in db.Occasions
        //                        from BouImg in bou.Messages
        //                        select new MessageViewModel()
        //                        {
        //                            MessageID = BouImg.MessageID,
        //                            MessageContent = BouImg.MessageContent,
        //                            OccasionID = BouImg.OccasionID,
        //                            OccasionName = bou.OccasionName
        //                        }).ToList();
        //    return messages;
        //}
    }
}