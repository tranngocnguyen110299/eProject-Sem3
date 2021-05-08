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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Flower
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flower()
        {
            this.BouquetDetails = new HashSet<BouquetDetail>();
        }
    
        public int FlowerID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter flower name")]
        [DisplayName("Flower Name")]
        public string FlowerName { get; set; }
        public int SupplierID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter short description")]
        [StringLength(maximumLength: 1000, MinimumLength = 1, ErrorMessage = "Short description must be less than 1000 characters")]
        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter unit price")]
        [DisplayName("Unit price")]
        [Range(maximum: Int64.MaxValue, minimum: 1, ErrorMessage = "Unit price must be a number")]
        public Nullable<int> UnitPrice { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter quantity")]
        [Range(maximum: Int64.MaxValue, minimum: 0, ErrorMessage = "Quantity must be a number")]
        public Nullable<int> Quantity { get; set; }
        public string Picture { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter meaning")]
        public string Meaning { get; set; }
        public bool Status { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BouquetDetail> BouquetDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
