using OnlineFloralDelivery.Models;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineFloralDelivery.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string pass)
        {
            OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

            ScryptEncoder encoder = new ScryptEncoder();
            ScryptEncoder encoder2 = new ScryptEncoder();

            var user = db.Employees.SingleOrDefault(model => model.Username == username);
            var user2 = db.Customers.SingleOrDefault(model => model.Username == username);

            if (user == null)
            {
                if (user2 == null)
                {
                    ViewBag.ErrorLogin = "Username or password incorrect";
                    return View();
                }
                else
                {
                    bool isValidPass2 = encoder2.Compare(pass, user2.Password);

                    if (isValidPass2)
                    {
                        if (user2.Status == false)
                        {
                            ViewBag.ErrorLogin = "Your account has been locked";
                            return View();
                        }

                        FormsAuthentication.SetAuthCookie(user2.Username, false);
                        var userSession = new Models.UserLogin();
                        userSession.Username = user2.Username;
                        userSession.FirstName = user2.FirstName;
                        userSession.LastName = user2.LastName;
                        userSession.Gender = user2.Gender;
                        userSession.DateOfBirth = user2.DateOfBirth;
                        userSession.Phone = user2.Phone;
                        userSession.Email = user2.Email;
                        userSession.Role = -1;
                        userSession.Address = user2.Address;
                        userSession.Status = user2.Status;
                        userSession.Picture = user2.Picture;
                        Session.Add(Common.CommonConstants.CLIENT_SESSION, userSession);
                        TempData["Notice_Login_Success"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorLogin = "Username or password incorrect";
                        return View();
                    }
                }
            }
            bool isValidPass = encoder.Compare(pass, user.Password);
            if (isValidPass)
            {
                if (user.Status == false)
                {
                    ViewBag.ErrorLogin = "Your account has been locked";
                    return View();
                }
                var userSession = new Models.UserLogin();
                userSession.Username = user.Username;
                userSession.FirstName = user.FirstName;
                userSession.LastName = user.LastName;
                userSession.Gender = user.Gender;
                userSession.DateOfBirth = user.DateOfBirth;
                userSession.Phone = user.Phone;
                userSession.Email = user.Email;
                userSession.Role = user.Role;
                userSession.Address = user.Address;
                userSession.Status = user.Status;
                userSession.Picture = user.Picture;
                Session.Add(Common.CommonConstants.CLIENT_SESSION, userSession);
                TempData["Notice_Login_Success"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorLogin = "Username or password incorrect";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session[Common.CommonConstants.CLIENT_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}