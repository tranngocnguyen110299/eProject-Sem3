using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using Scrypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        ImageProvider imgProvider = new ImageProvider();
        string ftpChild = "imgCustomers";

        public ActionResult Index()
        {
            if ((OnlineFloralDelivery.Models.UserLogin)Session[Common.CommonConstants.CLIENT_SESSION] == null)
            {
                return RedirectToAction("Index","Home");
            }
            var user = (OnlineFloralDelivery.Models.UserLogin)Session[Common.CommonConstants.CLIENT_SESSION];
            var userentity = db.Customers.Find(user.Username);
            var model = new UserProfile();
            model.FirstName = userentity.FirstName;
            model.LastName = userentity.LastName;
            model.Gender = userentity.Gender;
            model.DateOfBirth = userentity.DateOfBirth;
            model.Phone = userentity.Phone;
            model.Email = userentity.Email;
            model.Address = userentity.Address;
            model.Picture = imgProvider.LoadImage(userentity.Picture, ftpChild);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(UserProfile profile, string imgOld_User)
        {
            if (ModelState.IsValid)
            {
                if (profile.FirstName.Trim().Length == 0 || profile.LastName.Trim().Length == 0)
                {
                    TempData["Error"] = "Name is invalid";
                    return RedirectToAction("Index");
                }

                bool isAgeValid = true;
                if ((DateTime.Now.Year - profile.DateOfBirth.Value.Year) == 16)
                {
                    if ((DateTime.Now.Month - profile.DateOfBirth.Value.Month) == 0)
                    {
                        if ((DateTime.Now.Day - profile.DateOfBirth.Value.Day) > 0)
                        {
                            isAgeValid = false;
                        }
                    }
                    else if ((DateTime.Now.Month - profile.DateOfBirth.Value.Month) > 0)
                    {
                        isAgeValid = false;
                    }
                }
                else if ((DateTime.Now.Year - profile.DateOfBirth.Value.Year) < 16)
                {
                    isAgeValid = false;
                }

                if (!isAgeValid)
                {
                    TempData["Error"] = "Age must greater than 16 years old";
                    return RedirectToAction("Index");
                }

                var user = db.Customers.Find(profile.Username);

                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Gender = (bool)profile.Gender;
                user.DateOfBirth = profile.DateOfBirth;
                user.Phone = profile.Phone;
                user.Email = profile.Email;
                user.Address = profile.Address;
                user.Status = user.Status;

                string uploadFolderPath = Server.MapPath("~/public/uploadedFiles/userPictures/");
                if (profile.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(profile.ImageFile.FileName);
                    string extension = Path.GetExtension(profile.ImageFile.FileName);
                    if (imgProvider.Validate(profile.ImageFile) != null)
                    {
                        TempData["Error"] = imgProvider.Validate(profile.ImageFile);
                        return RedirectToAction("Index");
                    }
                    user.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //ftp.Update(user.Picture, ftpChild, profile.ImageFile, imgOld_User);
                    //DeleteImage(imgProvider.LoadImage(imgOld_User, ftpChild));
                    //SaveImage(user.ImageFile, user.Picture, ftpChild);
                    if (Directory.Exists(imgOld_User))
                    {
                        System.IO.File.Delete(Server.MapPath(imgOld_User));
                    }
                    

                    uploadFolderPath = Server.MapPath("~/public/uploadedFiles/imgCustomers/");

                    DeleteImage(imgProvider.LoadImage(user.Picture, ftpChild));
                    SaveImage(profile.ImageFile, user.Picture, ftpChild);

                }

                if (db.SaveChanges() > 0)
                {
                    TempData["SuccessMess"] = "Update successful";
                    var userSession = new Models.UserLogin();
                    userSession.Username = user.Username;
                    userSession.FirstName = user.FirstName;
                    userSession.LastName = user.LastName;
                    userSession.Gender = user.Gender;
                    userSession.DateOfBirth = user.DateOfBirth;
                    userSession.Phone = user.Phone;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    userSession.Role = -1;
                    userSession.Status = user.Status;
                    userSession.Picture = imgProvider.LoadImage(user.Picture, ftpChild);
                    Session.Add(Common.CommonConstants.CLIENT_SESSION, userSession);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string username, string currentPass, string newPass, string confirmPass)
        {
            if (username == null || currentPass == null || newPass == null || confirmPass == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (newPass != confirmPass)
            {
                TempData["Error"] = "Confirmation password does not match!";
                return RedirectToAction("ChangePassword");
            }

            ScryptEncoder encoder = new ScryptEncoder();
            var user = db.Customers.Find(username);
            bool isValidPass = encoder.Compare(currentPass, user.Password);
            if (!isValidPass)
            {
                TempData["Error"] = "Current password is incorrect!";
                return RedirectToAction("ChangePassword");
            }


            user.Password = encoder.Encode(newPass);
            if (db.SaveChanges() > 0)
            {
                TempData["SuccessMess"] = "Update successful";
            }
            return RedirectToAction("ChangePassword");

        }

        public ActionResult MyOrder()
        {
            if (Session[Common.CommonConstants.CLIENT_SESSION] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = (OnlineFloralDelivery.Models.UserLogin)Session[Common.CommonConstants.CLIENT_SESSION];
                var model = db.Orders.Where(i => i.Username == user.Username).ToList();
                return View(model);
            }
        }

        public ActionResult OrderDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Order orders = db.Orders.Find(id);
                var _orderDetail = (from ord in db.OrderDetails
                                    join or in db.Orders on ord.OrderID equals or.OrderID
                                    join pro in db.Bouquets on ord.BouquetID equals pro.BouquetID
                                    where ord.OrderID == orders.OrderID
                                    select ord).ToList();
                ViewBag.OrderDetail = _orderDetail;
                if (orders == null)
                {
                    return HttpNotFound();
                }
                return View(orders);
            }
        }

        public void SaveImage(HttpPostedFileBase imageFile, string fileName, string childNode)
        {
            string uploadFolderPath = Server.MapPath("~/public/uploadedFiles/" + childNode + "/");

            if (Directory.Exists(uploadFolderPath) == false)
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            fileName = Path.Combine(uploadFolderPath, fileName);

            imageFile.SaveAs(fileName);
        }

        public void DeleteImage(string path)
        {
            try
            {
                System.IO.File.Delete(Server.MapPath(path));
            }
            catch (Exception) { }

        }
    }
}