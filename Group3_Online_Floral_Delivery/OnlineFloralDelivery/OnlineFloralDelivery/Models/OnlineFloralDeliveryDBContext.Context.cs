﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineFloralDelivery.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineFloralDeliveryDBEntities : DbContext
    {
        public OnlineFloralDeliveryDBEntities()
            : base("name=OnlineFloralDeliveryDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BlogCategory> BlogCategories { get; set; }
        public virtual DbSet<BlogComment> BlogComments { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BouquetDetail> BouquetDetails { get; set; }
        public virtual DbSet<BouquetImage> BouquetImages { get; set; }
        public virtual DbSet<Bouquet> Bouquets { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Flower> Flowers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Occasion> Occasions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
