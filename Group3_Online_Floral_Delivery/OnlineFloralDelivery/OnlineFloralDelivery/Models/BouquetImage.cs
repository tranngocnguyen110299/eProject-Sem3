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

    public partial class BouquetImage
    {
        public int ImageID { get; set; }
        public int BouquetID { get; set; }
        [DisplayName("Image")]
        public string ImageFileName { get; set; }
    
        public virtual Bouquet Bouquet { get; set; }
    }
}
