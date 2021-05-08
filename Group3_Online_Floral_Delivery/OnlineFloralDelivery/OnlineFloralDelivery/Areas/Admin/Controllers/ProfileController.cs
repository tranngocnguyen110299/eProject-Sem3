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

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0,1")]
    public class ProfileController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgEmployees";

        // GET: Admin/Profile
        public ActionResult Index()
        {
            var user = (OnlineFloralDelivery.Areas.Admin.Models.UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var userentity = db.Employees.Find(user.Username);
            var model = new Profile();
            model.FirstName = userentity.FirstName;
            model.LastName = userentity.LastName;
            model.Gender = userentity.Gender;
            model.DateOfBirth = userentity.DateOfBirth;
            model.Phone = userentity.Phone;
            model.Email = userentity.Email;
            model.Address = userentity.Address;
            model.Picture = userentity.Picture;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(Profile profile, string imgOld_User)
        {
            if (ModelState.IsValid)
            {
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

                var user = db.Employees.Find(profile.Username);

                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Gender = (bool)profile.Gender;
                user.DateOfBirth = profile.DateOfBirth;
                user.Phone = profile.Phone;
                user.Email = profile.Email;
                user.Address = profile.Address;
                user.Status = user.Status;
                user.ImageFile = user.ImageFile;

                string uploadFolderPath = Server.MapPath("~/public/uploadedFiles/ftpChild/");
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
                    DeleteImage(imgProvider.LoadImage(user.Picture, ftpChild));
                    SaveImage(profile.ImageFile, user.Picture, ftpChild);

                }

                if (db.SaveChanges() > 0)
                {
                    TempData["Notice_Save_Success"] = true;
                    var userSession = new Models.UserLogin();
                    userSession.Username = user.Username;
                    userSession.FirstName = user.FirstName;
                    userSession.LastName = user.LastName;
                    userSession.Gender = user.Gender;
                    userSession.DateOfBirth = user.DateOfBirth;
                    userSession.Phone = user.Phone;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    userSession.Role = user.Role;
                    userSession.Status = user.Status;
                    userSession.Picture = imgProvider.LoadImage(user.Picture, ftpChild);
                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(string username, string currentPass, string newPass, string confirmPass)
        {
            if (username == null || currentPass == null || newPass == null || confirmPass == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ScryptEncoder encoder = new ScryptEncoder();
            var user = db.Employees.Find(username);
            bool isValidPass = encoder.Compare(currentPass, user.Password);
            if (!isValidPass)
            {
                TempData["Current_Pass_Fail"] = true;
                return RedirectToAction("Index");
            }
            if (newPass != confirmPass)
            {
                TempData["Password_Not_Match"] = true;
                return RedirectToAction("Index");
            }

            user.Password = encoder.Encode(newPass);
            if (db.SaveChanges() > 0)
            {
                TempData["Notice_Save_Success"] = true;
            }
            return RedirectToAction("Index");

        }

        public void DeleteImage(string path)
        {
            try
            {
                System.IO.File.Delete(Server.MapPath(path));
            }
            catch (Exception) { }




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
    }
}