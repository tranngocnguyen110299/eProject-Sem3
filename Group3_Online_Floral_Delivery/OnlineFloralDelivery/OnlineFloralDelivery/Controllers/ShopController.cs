using OnlineFloralDelivery.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineFloralDelivery.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string txtSearch, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            if (txtSearch != null)
            {
                var model = new BouquetDAO().Search(txtSearch).ToPagedList(pageNumber, pageSize);
                return View(model);
            }

            return View();
        }
    }
}