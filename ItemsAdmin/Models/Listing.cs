//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonsterAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Listing
    {
        public Listing()
        {
            this.ListingImages = new HashSet<ListingImage>();
            this.ListingSpecs = new HashSet<ListingSpec>();
            this.ListingOptions = new HashSet<ListingOption>();
        }
    
        public int id { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Currency { get; set; }
        public bool IsNegotiable { get; set; }
    
        public virtual RefCategory RefCategory { get; set; }
        public virtual ICollection<ListingImage> ListingImages { get; set; }
        public virtual ICollection<ListingSpec> ListingSpecs { get; set; }
        public virtual ICollection<ListingOption> ListingOptions { get; set; }
    }
}
