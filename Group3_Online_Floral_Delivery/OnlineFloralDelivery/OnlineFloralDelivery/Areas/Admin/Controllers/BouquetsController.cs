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

namespace OnlineFloralDelivery.Areas.Admin.Controllers
{
    [Authorize(Roles = "0,1")]
    public class BouquetsController : Controller
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private ImageProvider imgProvider = new ImageProvider();
        private string ftpChild = "imgMainBouquets";

        // GET: Admin/Bouquets
        public ActionResult Index()
        {
            var bouquets = db.Bouquets.Include(b => b.Sale).Include(b => b.Occasion);
            foreach (var item in bouquets)
            {
                item.MainImage = imgProvider.LoadImage(item.MainImage, ftpChild);
            }
            return View(bouquets.ToList());
        }

        // GET: Admin/Bouquets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bouquet bouquet = db.Bouquets.Find(id);
            bouquet.MainImage = imgProvider.LoadImage(bouquet.MainImage, ftpChild);
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            return View(bouquet);
        }

        // GET: Admin/Bouquets/Create
        public ActionResult Create()
        {
            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "SaleName");
            ViewBag.OccasionID = new SelectList(db.Occasions, "OccasionID", "OccasionName");
            var flower = db.Flowers.ToList();
            Bouquet bd = new Bouquet();
            bd.FlowerColection = flower;
            return View(bd);
        }

        // POST: Admin/Bouquets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "BouquetID,BouquetName,OccasionID,SaleID,MainImage,UnitPrice,OldUnitPrice,ShortDescription,Description,Quantity,Status,ImageFile,SelectedIDArray")] Bouquet bouquet)
        {
            if (ModelState.IsValid)
            {
                if(bouquet.SelectedIDArray == null)
                {
                    TempData["Error"] = "Please choose the occasion";
                    return RedirectToAction("Create");
                }
                string fileName = Path.GetFileNameWithoutExtension(bouquet.ImageFile.FileName);
                string extension = Path.GetExtension(bouquet.ImageFile.FileName);

                if (imgProvider.Validate(bouquet.ImageFile) != null)
                {
                    ViewBag.Error = imgProvider.Validate(bouquet.ImageFile);
                    return View("Create");
                }
                bouquet.MainImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                bouquet.Quantity = -1;
                try
                {
                    var id = Insert(bouquet);
                    SaveImage(bouquet.ImageFile, bouquet.MainImage, ftpChild);
                    //
                    foreach (var item in bouquet.SelectedIDArray)
                    {
                        BouquetDetail bouquetDetail = new BouquetDetail();
                        bouquetDetail.BouquetID = (int)id;
                        bouquetDetail.FlowerID = Int32.Parse(item);
                        bouquetDetail.Quantity = 0;
                        db.BouquetDetails.Add(bouquetDetail);
                        Insert2(bouquetDetail);
                    }
                    return RedirectToAction("FlowerQuanityDetails", new { id = bouquet.BouquetID });
                }
                catch (Exception)
                {
           
                }

                return RedirectToAction("Index");
            }

            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "SaleName", bouquet.SaleID);
            ViewBag.OccasionID = new SelectList(db.Occasions, "OccasionID", "OccasionName", bouquet.OccasionID);
            var flowers = db.Flowers.ToList();
            Bouquet bd = new Bouquet();
            bd.FlowerColection = flowers;
            return View(bd);
        }

        public ActionResult DeleteBouquetDetail(int id)
        {
            try
            {
                var flower = db.BouquetDetails.Where(m => m.BouquetID == id).ToList();
                foreach (var item in flower)
                {
                    db.BouquetDetails.Remove(item);
                }

                Bouquet bou = db.Bouquets.Find(id);
                db.Bouquets.Remove(bou);

                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(bou.MainImage, ftpChild));
                }
            }
            catch (Exception)
            {
            }

            return RedirectToAction("Index");
        }
        public ActionResult FlowerQuanityDetails(int id)
        {
            var bouquet = db.BouquetDetails.Where(m=>m.BouquetID == id).ToList();
            int i = 0;
            foreach (var item in bouquet)
            {
                ++i;
            }
            int a = i;
            ViewBag.FlowerDetails = bouquet;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult FlowerQuanityDetails([Bind(Include = "BouquetID,FlowerID,Quantity,SelectedIDArray,SelectedIDArrayFlower,SelectedIDArrayBouquet")] BouquetDetail bouquetDetail)
        {
            if (ModelState.IsValid)
            {
                int y = 0;
                for (int i = 0; i < bouquetDetail.SelectedIDArray.Length/3; i++)
                {
                    BouquetDetail bouDetail = new BouquetDetail();
                    
                    bouDetail.FlowerID = Int32.Parse(bouquetDetail.SelectedIDArray[y++]);
                    bouDetail.BouquetID = Int32.Parse(bouquetDetail.SelectedIDArray[y++]);
                    bouDetail.Quantity = Int32.Parse(bouquetDetail.SelectedIDArray[y++]);

                    
                    db.Entry(bouDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    //----
                }

                try
                {

                    Bouquet bou = db.Bouquets.Find(Int32.Parse(bouquetDetail.SelectedIDArray[1]));
                    bou.Quantity = 0;
                    bou.BouquetName = bou.BouquetName;
                    bou.OccasionID = bou.OccasionID;
                    bou.SaleID = bou.SaleID;
                    bou.MainImage = bou.MainImage;
                    bou.UnitPrice = bou.UnitPrice;
                    bou.OldUnitPrice = bou.OldUnitPrice;
                    bou.ShortDescription = bou.ShortDescription;
                    bou.Description = bou.Description;
                    bou.Status = bou.Status;
                    db.Entry(bou).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            TempData["Notice_Save_Success"] = true;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Import", new { id = bouquetDetail.FlowerID });
        }

        public ActionResult Import()
        {
            //var importation = db.Importation.Include(i => i.Products).Include(i => i.Users);
            ViewBag.BouquetList = new List<Bouquet>(db.Bouquets.Include(i => i.BouquetImages).Where(i=>i.Quantity >=0).ToList());
            return View();
        }

        public ActionResult CreateImport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bouquet = db.Bouquets.FirstOrDefault(p => p.BouquetID == id);
            ViewBag.Bouquet = bouquet;
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        public ActionResult CreateExport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bouquet = db.Bouquets.FirstOrDefault(p => p.BouquetID == id);
            ViewBag.Bouquet = bouquet;
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateImport([Bind(Include = "BouquetID,BouquetName,OccasionID,SaleID,MainImage,UnitPrice,OldUnitPrice,ShortDescription,Description,Quantity,Status")] Bouquet bouquet)
        {
            
            try {
                if (bouquet.Quantity < 0 || bouquet.Quantity.ToString().Length == 0)
                {
                    TempData["Error"] = "Number of products must be greater than or equal to 0";
                    return RedirectToAction("CreateImport", new { id = bouquet.BouquetID });
                }

                Bouquet bou = db.Bouquets.Single(p => p.BouquetID == bouquet.BouquetID);
            var bd = db.BouquetDetails.Where(m => m.BouquetID == bouquet.BouquetID).ToList();
                foreach (var item in bd)
                {
                    var flo = db.Flowers.Single(m => m.FlowerID == item.FlowerID);
                   
                    if ((int)item.Quantity * (int)bouquet.Quantity > (int)flo.Quantity)
                    {
                        TempData["Error"] = "Exceeded number of flowers in stock - " + flo.FlowerName;
                        return RedirectToAction("CreateImport", new { id = bouquet.BouquetID });
                    }
                }

                foreach (var item in bd)
                {
                    var flo = db.Flowers.Single(m => m.FlowerID == item.FlowerID);

                    flo.Quantity -= (int)item.Quantity * (int)bouquet.Quantity;
                    db.Entry(flo).State = EntityState.Modified;
                    db.Entry(flo).Property(x => x.FlowerName).IsModified = false;
                    db.Entry(flo).Property(x => x.SupplierID).IsModified = false;
                    db.Entry(flo).Property(x => x.ShortDescription).IsModified = false;
                    db.Entry(flo).Property(x => x.UnitPrice).IsModified = false;
                    db.Entry(flo).Property(x => x.Picture).IsModified = false;
                    db.Entry(flo).Property(x => x.Meaning).IsModified = false;
                    db.Entry(flo).Property(x => x.Status).IsModified = false;
                }

                    bou.Quantity += bouquet.Quantity;
                    db.Entry(bou).State = EntityState.Modified;
                    db.Entry(bou).Property(x => x.BouquetName).IsModified = false;
                    db.Entry(bou).Property(x => x.OccasionID).IsModified = false;
                    db.Entry(bou).Property(x => x.SaleID).IsModified = false;
                    db.Entry(bou).Property(x => x.MainImage).IsModified = false;
                    db.Entry(bou).Property(x => x.UnitPrice).IsModified = false;
                    db.Entry(bou).Property(x => x.OldUnitPrice).IsModified = false;
                    db.Entry(bou).Property(x => x.ShortDescription).IsModified = false;
                    db.Entry(bou).Property(x => x.Description).IsModified = false;
                    db.Entry(bou).Property(x => x.Status).IsModified = false;
                    db.SaveChanges();

                    TempData["Notice_Save_Success"] = true;
                    return RedirectToAction("Import");
            }
            catch (Exception)
            {
                return RedirectToAction("CreateImport", new { id = bouquet.BouquetID });
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateExport([Bind(Include = "BouquetID,BouquetName,OccasionID,SaleID,MainImage,UnitPrice,OldUnitPrice,ShortDescription,Description,Quantity,Status")] Bouquet bouquet)
        {
            try
            {

                Bouquet bou = db.Bouquets.Single(p => p.BouquetID == bouquet.BouquetID);
                if (bouquet.Quantity > bou.Quantity )
                {
                    TempData["Error"] = "Quantity must be less than unit in stock";
                    return RedirectToAction("CreateExport", new { id = bouquet.BouquetID });
                }
                else
                {
                    bou.Quantity -= bouquet.Quantity;
                    db.Entry(bou).State = EntityState.Modified;
                    db.Entry(bou).Property(x => x.BouquetName).IsModified = false;
                    db.Entry(bou).Property(x => x.OccasionID).IsModified = false;
                    db.Entry(bou).Property(x => x.SaleID).IsModified = false;
                    db.Entry(bou).Property(x => x.MainImage).IsModified = false;
                    db.Entry(bou).Property(x => x.UnitPrice).IsModified = false;
                    db.Entry(bou).Property(x => x.OldUnitPrice).IsModified = false;
                    db.Entry(bou).Property(x => x.ShortDescription).IsModified = false;
                    db.Entry(bou).Property(x => x.Description).IsModified = false;
                    db.Entry(bou).Property(x => x.Status).IsModified = false;
                    db.SaveChanges();
                    TempData["Notice_Save_Success"] = true;

                    return RedirectToAction("Import");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("CreateExport", new { id = bouquet.BouquetID });
            }


        }

        public long Insert(Bouquet bouquet)
        {
            db.Bouquets.Add(bouquet);
            db.SaveChanges();
            return bouquet.BouquetID;
        }

        public bool Insert2(BouquetDetail detail)
        {
            db.BouquetDetails.Add(detail);
            if (db.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // GET: Admin/Bouquets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bouquet bouquet = db.Bouquets.Find(id);
            Session["old_MainImage"] = bouquet.MainImage;
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "SaleName", bouquet.SaleID);
            ViewBag.OccasionID = new SelectList(db.Occasions, "OccasionID", "OccasionName", bouquet.OccasionID);
            return View(bouquet);
        }

        // POST: Admin/Bouquets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "BouquetID,BouquetName,OccasionID,SaleID,MainImage,UnitPrice,OldUnitPrice,ShortDescription,Description,Quantity,Status,ImageFile")] Bouquet bouquet, String old_MainImage)
        {
            if (ModelState.IsValid)
            {
                if (bouquet.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(bouquet.ImageFile.FileName);
                    string extension = Path.GetExtension(bouquet.ImageFile.FileName);

                    if (imgProvider.Validate(bouquet.ImageFile) != null)
                    {
                        ViewBag.Error = imgProvider.Validate(bouquet.ImageFile);
                        return View("Edit");
                    }
                    bouquet.MainImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    DeleteImage(imgProvider.LoadImage(old_MainImage, ftpChild));
                    SaveImage(bouquet.ImageFile, bouquet.MainImage, ftpChild);
                }
                else
                {
                    bouquet.MainImage = old_MainImage;
                }
                db.Entry(bouquet).State = EntityState.Modified;
                db.Entry(bouquet).Property(x => x.Quantity).IsModified = false;
                if (db.SaveChanges() > 0)
                {
                    TempData["Notice_Save_Success"] = true;
                    Session.Remove("old_MainImage");
                }
                return RedirectToAction("Index");
            }
            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "SaleName", bouquet.SaleID);
            ViewBag.OccasionID = new SelectList(db.Occasions, "OccasionID", "OccasionName", bouquet.OccasionID);
            return View(bouquet);
        }

        // GET: Admin/Bouquets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bouquet bouquet = db.Bouquets.Find(id);
            bouquet.MainImage = imgProvider.LoadImage(bouquet.MainImage, ftpChild);
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            return View(bouquet);
        }

        // POST: Admin/Bouquets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Bouquet bouquet = db.Bouquets.Find(id);
                db.Bouquets.Remove(bouquet);
                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(bouquet.MainImage, ftpChild));
                    TempData["Notice_Delete_Success"] = true;
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                TempData["Notice_Delete_Fail"] = true;
            }
            catch (Exception)
            {
                TempData["Notice_Delete_Fail"] = true;
            }
            return RedirectToAction("Index");
        }

        public ActionResult BouquetImages(int? id)
        {
            if (id == null || db.Bouquets.Find(id) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var BouquetImage = db.BouquetImages.Include(p => p.Bouquet).Where(m => m.BouquetID == id);
            foreach (var item in BouquetImage)
            {
                item.ImageFileName = imgProvider.LoadImage(item.ImageFileName, "imgBouquets");
            }
            string[] bouquetinfo = new string[] { id.ToString(), db.Bouquets.Find(id).BouquetName.ToString() };
            ViewBag.Bouquetinfo = bouquetinfo;

            return View(BouquetImage.ToList());
        }

        [HttpPost]
        public ActionResult AddImage(int BouquetID, HttpPostedFileBase ImageFile)
        {
            BouquetImage bouquetImage = new BouquetImage();
            bouquetImage.BouquetID = BouquetID;

            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            string ftpChild = "imgBouquets";

            if (imgProvider.Validate(ImageFile) != null)
            {
                TempData["Error"] = imgProvider.Validate(ImageFile);
                return RedirectToAction("BouquetImages", "Bouquets", new { @id = bouquetImage.BouquetID });
            }
            bouquetImage.ImageFileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            db.BouquetImages.Add(bouquetImage);
            if (db.SaveChanges() > 0)
            {
                SaveImage(ImageFile, bouquetImage.ImageFileName, ftpChild);
                TempData["Notice_Save_Success"] = true;
            }
            return RedirectToAction("BouquetImages", "Bouquets", new { @id = bouquetImage.BouquetID });
        }

        [HttpPost, ActionName("DeleteImage")]
        public ActionResult DeleteImage(int bouImage, int bouID)
        {
            try
            {
                BouquetImage bouquetImage = db.BouquetImages.Find(bouImage);
                db.BouquetImages.Remove(bouquetImage);
                if (db.SaveChanges() > 0)
                {
                    DeleteImage(imgProvider.LoadImage(bouquetImage.ImageFileName, "imgBouquets"));
                    TempData["Notice_Delete_Success"] = true;
                }
            }
            catch (Exception)
            {
                TempData["Notice_Delete_Fail"] = true;
            }

            return RedirectToAction("BouquetImages", "Bouquets", new { @id = bouID });
        }

        //public ActionResult CreateBouquetDetails(int? id)
        //{

        //    if (id == null || db.Bouquets.Find(id) == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var BouquetImage = db.BouquetImages.Include(p => p.Bouquet).Where(m => m.BouquetID == id);
        //    var flower = db.Flowers.ToList();
        //    foreach (var item in BouquetImage)
        //    {
        //        item.ImageFileName = imgProvider.LoadImage(item.ImageFileName, "imgBouquets");
        //    }
        //    string[] bouquetinfo = new string[] { id.ToString(), db.Bouquets.Find(id).BouquetName.ToString() };
        //    ViewBag.Bouquetinfo = bouquetinfo;
        //    BouquetDetail bd = new BouquetDetail();
        //    bd.FlowerColection = flower;
        //    return View(bd);
        //}

        

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
