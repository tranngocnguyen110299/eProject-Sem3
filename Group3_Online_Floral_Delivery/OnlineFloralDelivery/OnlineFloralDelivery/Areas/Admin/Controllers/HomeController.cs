using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0,1")]
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return RedirectToAction("Index","Profile");
        }
    }
}