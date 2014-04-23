using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Framework.Model
{
    public class Image
    {
        public int id { get; set; }

        public string Src { get; set; }

        public string FileName { get; set; }

        public bool IsCover { get; set; }
    }

    public class Images
    {
        public List<Image> ImageList { get; set; }
    }
}
