using OnlineFloralDelivery.Models;
using OnlineFloralDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.DAO
{
    public class BlogCommentDAO
    {
        private OnlineFloralDeliveryDBEntities db = new OnlineFloralDeliveryDBEntities();
        public bool Insert(BlogComment blogComment)
        {
            blogComment.Date = DateTime.Now;
            db.BlogComments.Add(blogComment);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public List<BlogCommentViewModel> getCommentList(int? id)
        {
            var CommentOfBlog = (from blog in db.Blogs
                                 join blogComm in db.BlogComments on blog.BlogID equals blogComm.BlogID
                                 where blog.BlogID == id
                                 orderby blogComm.Date descending
                                 select new BlogCommentViewModel()
                                 {
                                     BlogCommentID = blogComm.BlogComentID,
                                     Username = blogComm.Username,
                                     Content = blogComm.Content,
                                     Date = blogComm.Date,
                                     BlogID = blogComm.BlogID
                                 }).ToList();
            return CommentOfBlog;
        }
    }
}