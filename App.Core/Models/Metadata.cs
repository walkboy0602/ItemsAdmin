using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace App.Core.Data
{

    [MetadataType(typeof(RefCategoryMetaData))]
    public partial class RefCategory
    {
        public Nullable<int> ChildrenCount { get; set; }
    }

    [MetadataType(typeof(ListingMetaData))]
    public partial class Listing
    {

    }

    public class ListingMetaData
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please choose a category.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please select a currency.")]
        public string Currency { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage= "You can only enter up to 2 decimal number.")]
        [Range(0, 9999999.99)]
        [Required(ErrorMessage = "Please enter price.")]
        public decimal Price { get; set; }

        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime UpdateDate { get; set; }
    }

    public class RefCategoryMetaData
    {
        public int id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be at least {1} characters long.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public Nullable<int> ParentID { get; set; }

        //[Required(ErrorMessage=String.Format("The 'something' ({0}) is required because of: {1}", "1233213"))]
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string Description { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<bool> isActive { get; set; }
    }

    public class CategoryGroup
    {
        public RefCategory Value { get; set; }
        public IEnumerable<CategoryGroup> Children { get; set; }
    }


}