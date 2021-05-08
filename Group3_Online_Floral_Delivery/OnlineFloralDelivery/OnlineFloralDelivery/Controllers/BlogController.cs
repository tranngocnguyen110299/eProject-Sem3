using OnlineFloralDelivery.DAO;
using OnlineFloralDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using OnlineFloralDelivery.Providers;

namespace OnlineFloralDelivery.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();

        //CategoryDAO categoryDAO = new CategoryDAO();
        //SupplierDAO supplierDAO = new SupplierDAO();
        BlogCategoriesDAO blogCategoriesDAO = new BlogCategoriesDAO();
        BlogDAO blogDAO = new BlogDAO();
        BlogCommentDAO blogCommentDAO = new BlogCommentDAO();

        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.BlogCategoies = blogCategoriesDAO.getAllBlogCategories();
            var model = blogDAO.getBlog().ToPagedList(pageNumber, pageSize);
            TempData["BlogsActive"] = "active";
            return View(model);
        }

        public ActionResult BlogList(int? id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (db.BlogCategories.Find(id) == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var model = blogDAO.getBlogOfBlogCategories(id).ToPagedList(pageNumber, pageSize);
            ViewBag.BlogCategoies = blogCategoriesDAO.getAllBlogCategories();
            ViewBag.BlogCategoryName = db.BlogCategories.Find(id).BlogCategoriesName;
            ViewBag.CurrentBlogCate = id;
            return View(model);
        }

        public ActionResult BlogDetail(int? id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            ViewBag.BlogCategoies = blogCategoriesDAO.getAllBlogCategories();
            var blogDetail = db.Blogs.Find(id);
            blogDetail.Picture = new ImageProvider().LoadImage(blogDetail.Picture, "imgBlogs");
            ViewBag.Blog = blogDetail;

            var model = blogCommentDAO.getCommentList(id).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddComment(string txtFullName, string txaComment, int? BlogID)
        {
            if (BlogID == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if (txtFullName == null || txaComment == null)
            {
                ViewBag.ErrorMessage = "Please complete the form before submitting";
                return RedirectToAction("BlogDetail", "Blog", new { id = BlogID });
            }
            var cmt = new BlogComment();

            cmt.Username = txtFullName;
            cmt.Content = txaComment;
            cmt.BlogID = (int)BlogID;
            var blogComment = new BlogCommentDAO();
            bool isValid = false;
            try
            {
                isValid = blogComment.Insert(cmt);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Please complete the form before submitting";
                return RedirectToAction("BlogDetail", "Blog", new { id = BlogID });
            }

            if (isValid == true)
            {
                TempData["Notice_Submit_Success"] = true;
                return RedirectToAction("BlogDetail", "Blog", new { id = cmt.BlogID });
            }

            return RedirectToAction("BlogDetail", "Blog", new { id = cmt.BlogID });
        }
    }
}