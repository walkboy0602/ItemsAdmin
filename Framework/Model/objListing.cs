using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Framework.Model
{
    public class Listing
    {
        public int id { get; set; }

        public int SaleType { get; set; }

        [Required(ErrorMessage = "Please choose a Category.")]
        [Range(0, 10000)]
        public int? CategoryType { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.0, Double.MaxValue)]
        public decimal? Price { get; set; }

        public Int16 isWarrently { get; set; }

        public string WarrentyDesc { get; set; }

        public bool COD { get; set; }

        public bool OnPay { get; set; }

        public string PaymentDescription { get; set; }

        public int UserID { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Image> Image { get; set; }

        public string ImageUrl { get; set; }
    }
}
