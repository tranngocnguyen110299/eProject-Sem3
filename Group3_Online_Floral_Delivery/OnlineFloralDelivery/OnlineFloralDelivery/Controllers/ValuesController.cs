using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class ValuesController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        // GET: Values
        public ActionResult Index()
        {
            return View();
        }

        public String CheckUsername(String username)
        {
            if (username.Trim().Length < 6 || username.Trim().Length > 50)
            {
                return "Username must be between 6 to 50 characters";
            }
            else
            {
                if (db.Employees.Find(username) != null || db.Customers.Find(username) != null)
                    return "Username already exists";
                else
                    return null;
            }

        }

        public String CheckUsernameOfBlogs(String username)
        {
            if (db.Employees.Find(username) == null)
            {
                return "Username no exist";
            }
            else
            {
                return null;
            }
        }

        public String CheckPassword(String password)
        {
            if (password.Trim().Length < 8 || password.Trim().Length > 50)
                return "Password must be between 8 to 50 characters";
            else
                return null;
        }

        public String CheckEmail(String emailAddress)
        {
            bool isEmail = Regex.IsMatch(emailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                bool isExist = db.Employees.ToList().Exists(model => model.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
                bool isExist2 = db.Customers.ToList().Exists(model => model.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
                if (isExist || isExist2)
                {
                    return "Email already exists";
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return "Email is invalid";
            }
        }

        //public String DisplayFlowerQuantity(BouquetDetail flower)
        //{
        //    BouquetDetail bd = new BouquetDetail();
        //    bd.FlowerID = int(flower.SelectedIDArray);
        //    if (true)
        //    {
        //        return "Code has been exist";
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


    }
}