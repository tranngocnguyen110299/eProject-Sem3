using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using Scrypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFloralDelivery.Controllers
{
    public class RegisterController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        ImageProvider imgProvider = new ImageProvider();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Username,Password,FirstName,LastName,DateOfBirth,Gender,Phone,Email,Address,Picture,Status, ImageFile")] Customer users)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.Find(users.Username) != null || db.Employees.Find(users.Username) != null)
                {
                    TempData["ErrorMess"] = "Username already exists";
                    return View(users);
                }

                if (users.Username.Length < 6 || users.Username.Length > 50)
                {
                    TempData["ErrorMess"] = "Username must be between 6 to 50 characters.";
                    return View(users);
                }

                bool isExist = db.Customers.ToList().Exists(model => model.Email.Equals(users.Email, StringComparison.OrdinalIgnoreCase));
                bool isExist2 = db.Employees.ToList().Exists(model => model.Email.Equals(users.Email, StringComparison.OrdinalIgnoreCase));
                if (isExist || isExist2)
                {
                    TempData["ErrorMess"] = "Email already exists";
                    return View(users);
                }

                bool isAgeValid = true;
                if ((DateTime.Now.Year - users.DateOfBirth.Value.Year) == 16)
                {
                    if ((DateTime.Now.Month - users.DateOfBirth.Value.Month) == 0)
                    {
                        if ((DateTime.Now.Day - users.DateOfBirth.Value.Day) > 0)
                        {
                            isAgeValid = false;
                        }
                    }
                    else if ((DateTime.Now.Month - users.DateOfBirth.Value.Month) > 0)
                    {
                        isAgeValid = false;
                    }
                }
                else if ((DateTime.Now.Year - users.DateOfBirth.Value.Year) < 16)
                {
                    isAgeValid = false;
                }

                if (!isAgeValid)
                {
                    TempData["ErrorMess"] = "Age must greater than 16 years old";
                    return View(users);
                }

                if (users.Password.Length < 8 || users.Password.Length > 50)
                {
                    TempData["ErrorMess"] = "Password must be between 8 to 50 characters";
                    return View(users);
                }

                string fileName = Path.GetFileNameWithoutExtension(users.ImageFile.FileName);
                string extension = Path.GetExtension(users.ImageFile.FileName);
                if (imgProvider.Validate(users.ImageFile) != null)
                {
                    TempData["ErrorMess"] = imgProvider.Validate(users.ImageFile);
                    return View(users);
                }

                users.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                ScryptEncoder encoder = new ScryptEncoder();
                users.Password = encoder.Encode(users.Password);


                Customer custumner = new Customer();
                custumner.Username = users.Username;
                custumner.Password = users.Password;
                custumner.FirstName = users.FirstName;
                custumner.LastName = users.LastName;
                custumner.Gender = users.Gender;
                custumner.DateOfBirth = users.DateOfBirth;
                custumner.Phone = users.Phone;
                custumner.Email = users.Email;
                custumner.Address = users.Address;
                custumner.Picture = users.Picture;
                custumner.Status = true;
                db.Customers.Add(custumner);

                if (db.SaveChanges() > 0)
                {
                    SaveImage(users.ImageFile, users.Picture, "imgCustomers");
                    TempData["SuccessMess"] = "Register successfully";
                }
                return View();
            }

            return View();
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