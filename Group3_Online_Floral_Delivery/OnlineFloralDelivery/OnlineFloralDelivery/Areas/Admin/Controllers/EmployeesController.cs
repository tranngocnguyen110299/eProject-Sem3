using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using Scrypt;

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0")]
    public class EmployeesController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgEmployees";

        // GET: Admin/Employees
        public ActionResult Index()
        {
            var list = db.Employees.ToList();
            foreach (var item in list)
            {
                item.Picture = imgProvider.LoadImage(item.Picture, ftpChild);
            }
            return View(list);
        }

        // GET: Admin/Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            employee.Picture = imgProvider.LoadImage(employee.Picture, ftpChild);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,FirstName,LastName,DateOfBirth,Gender,Phone,Email,Address,Picture,Role,Status, ImageFile")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (db.Employees.Find(employee.Username) != null || db.Customers.Find(employee.Username) != null)
                {
                    ViewBag.Error = "Username already exists";
                    return View("Create");
                }

                if (employee.Username.Length < 8 || employee.Username.Length > 50)
                {
                    ViewBag.Error = "Username must be between 8 to 50 characters.";
                    return View("Create");
                }

                bool isExist = db.Employees.ToList().Exists(model => model.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase));
                bool isExist2 = db.Customers.ToList().Exists(model => model.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase));
                if (isExist || isExist2)
                {
                    ViewBag.Error = "Email already exists";
                    return View("Create");
                }

                bool isAgeValid = true;
                if ((DateTime.Now.Year - employee.DateOfBirth.Value.Year) == 16)
                {
                    if ((DateTime.Now.Month - employee.DateOfBirth.Value.Month) == 0)
                    {
                        if ((DateTime.Now.Day - employee.DateOfBirth.Value.Day) > 0)
                        {
                            isAgeValid = false;
                        }
                    }
                    else if ((DateTime.Now.Month - employee.DateOfBirth.Value.Month) > 0)
                    {
                        isAgeValid = false;
                    }
                }
                else if ((DateTime.Now.Year - employee.DateOfBirth.Value.Year) < 16)
                {
                    isAgeValid = false;
                }

                if (!isAgeValid)
                {
                    ViewBag.Error = "Age must greater than 16 years old";
                    return View("Create");
                }

                if (employee.Password.Length < 8 || employee.Password.Length > 50)
                {
                    ViewBag.Error = "Password must be between 8 to 50 characters";
                    return View("Create");
                }
                string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                string extension = Path.GetExtension(employee.ImageFile.FileName);
                if (imgProvider.Validate(employee.ImageFile) != null)
                {
                    ViewBag.Error = imgProvider.Validate(employee.ImageFile);
                    return View(employee);
                }
                employee.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                ScryptEncoder encoder = new ScryptEncoder();
                employee.Password = encoder.Encode(employee.Password);

                db.Employees.Add(employee);
                if (db.SaveChanges() > 0)
                {
                    SaveImage(employee.ImageFile, employee.Picture, ftpChild);
                    TempData["Notice_Create_Success"] = true;
                }
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            Session["OldImage_User"] = employee.Picture;
            return View(employee);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password,FirstName,LastName,DateOfBirth,Gender,Phone,Email,Address,Picture,Role,Status, ImageFile")] Employee employee, String imageOldFile_User)
        {
            if (ModelState.IsValid)
            {
                bool isAgeValid = true;
                if ((DateTime.Now.Year - employee.DateOfBirth.Value.Year) == 16)
                {
                    if ((DateTime.Now.Month - employee.DateOfBirth.Value.Month) == 0)
                    {
                        if ((DateTime.Now.Day - employee.DateOfBirth.Value.Day) > 0)
                        {
                            isAgeValid = false;
                        }
                    }
                    else if ((DateTime.Now.Month - employee.DateOfBirth.Value.Month) > 0)
                    {
                        isAgeValid = false;
                    }
                }
                else if ((DateTime.Now.Year - employee.DateOfBirth.Value.Year) < 16)
                {
                    isAgeValid = false;
                }

                if (!isAgeValid)
                {
                    ViewBag.Error = "Age must greater than 16 years old";
                    return View("Edit");
                }

                //string uploadFolderPath = Server.MapPath("~/public/uploadedFiles/imgEmployees/");
                if (employee.ImageFile == null)
                {
                    employee.Picture = imageOldFile_User;
                }
                else
                { 
                    string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                    string extension = Path.GetExtension(employee.ImageFile.FileName);
                    if (imgProvider.Validate(employee.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(employee.ImageFile);
                        return View("Edit");
                    }
                    employee.Picture = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    DeleteImage(imgProvider.LoadImage(imageOldFile_User, ftpChild));
                    SaveImage(employee.ImageFile, employee.Picture, ftpChild);
                }
                //employee.Password = "okokokokokok";
                var emp = db.Employees.Select(p => p).Where(p => p.Username == employee.Username).FirstOrDefault();
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.Gender = employee.Gender;
                emp.Phone = employee.Phone;
                emp.Email = employee.Email;
                emp.Address = employee.Address;
                emp.Picture = employee.Picture;
                emp.Role = employee.Role;
                emp.Status = employee.Status;
                //db.Entry(employee).State = EntityState.Modified;
                //db.Entry(employee).Property(x => x.Password).IsModified = true;
                if (db.SaveChanges() > 0)
                {
                    Session.Remove("OldImage_User");
                    TempData["Notice_Save_Success"] = true;
                }
                //TempData["Notice_Save_Success"] = true;
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            employee.Picture = imgProvider.LoadImage(employee.Picture, ftpChild);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(employee.Picture, ftpChild));
                    TempData["Notice_Delete_Success"] = true;
                }
            }
            catch (Exception)
            {
                TempData["Notice_Delete_Fail"] = true;
            }

            return RedirectToAction("Index");
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
