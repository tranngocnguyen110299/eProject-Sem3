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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int OrderID { get; set; }
        public string Username { get; set; }
        public int PaymentID { get; set; }
        public string Message { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Recipient { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public System.DateTime ShippingDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Payment Payment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
