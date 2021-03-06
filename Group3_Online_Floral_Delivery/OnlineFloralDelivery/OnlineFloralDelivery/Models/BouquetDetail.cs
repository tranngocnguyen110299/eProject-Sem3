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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class BouquetDetail
    {
        public int BouquetID { get; set; }
        public int FlowerID { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public IEnumerable<Flower> FlowerColection { get; set;}
        [NotMapped]
        public string[] SelectedIDArray { get; set; }
        public string[] SelectedIDArrayBouquet { get; set; }
        public string[] SelectedIDArrayFlower { get; set; }

        public virtual Bouquet Bouquet { get; set; }
        public virtual Flower Flower { get; set; }
    }
}
