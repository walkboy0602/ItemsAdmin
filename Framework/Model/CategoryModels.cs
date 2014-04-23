using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Framework.Model
{
    public class CategoryModels
    {
        [Required]
        public string Name { get; set; }

        public int ParentID { get; set; }

        public string MetaDesc { get; set; }

        public string MetaKeyword { get; set; }

        public string Description { get; set; }
    }
}
