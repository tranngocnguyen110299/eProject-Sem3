using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class OccasionDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

        public List<OccasionViewModel> Get()
        {
            var list = (from occ in db.Occasions
                        select new OccasionViewModel
                        {
                            OccasionID = occ.OccasionID,
                            OccasionName = occ.OccasionName,
                        }).ToList();
            return list;
        }

    }
}