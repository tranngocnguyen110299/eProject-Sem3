//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int OrderID { get; set; }
        public int BouquetID { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    
        public virtual Bouquet Bouquet { get; set; }
        public virtual Order Order { get; set; }
    }
}