using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.Providers;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class BlogDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        private string ftpChild = "imgBlogs";
        public List<BlogViewModel> getBlog()
        {
            var list = (from blog in db.Blogs
                        orderby blog.Date descending
                        select new BlogViewModel()
                        {
                            BlogID = blog.BlogID,
                            BlogName = blog.BlogName,
                            Username = blog.Username,
                            Content = blog.Content,
                            BlogCategoriesID = blog.BlogCategoriesID,
                            Date = blog.Date,
                            Picture = blog.Picture
                        }).ToList();
            foreach (var item in list)
            {
                //item.Thumbnail = ftp.Get(item.Thumbnail, ftpChild);
                item.Picture = new ImageProvider().LoadImage(item.Picture, ftpChild);
            }
            return list;
        }

        public List<BlogViewModel> getBlogOfBlogCategories(int? id)
        {
            var list = (from blogcate in db.BlogCategories
                        join blog in db.Blogs on blogcate.BlogCategoriesID equals blog.BlogCategoriesID
                        where blog.BlogCategoriesID == id
                        select new BlogViewModel()
                        {
                            BlogID = blog.BlogID,
                            BlogName = blog.BlogName,
                            Username = blog.Username,
                            Content = blog.Content,
                            BlogCategoriesID = blog.BlogCategoriesID,
                            Date = blog.Date,
                            Picture = blog.Picture
                        }).ToList();
            foreach (var item in list)
            {
                //item.Thumbnail = ftp.Get(item.Thumbnail, ftpChild);
                item.Picture = new ImageProvider().LoadImage(item.Picture, ftpChild);
            }
            return list;
        }

    }
}