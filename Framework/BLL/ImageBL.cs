using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DAL;
using Framework.Model;

namespace Framework.BLL
{
    public class ImageBL
    {
        ImageDL _ImageDL;

        public ImageBL()
        {
            _ImageDL = new ImageDL();
        }

        public bool Delete(int ImageID)
        {
            return _ImageDL.Delete(ImageID);
        }

        public Image SaveImage(int ListingID, string FileName, string Src)
        {
            bool result = _ImageDL.SaveImage(ListingID, FileName, Src);

            if (result)
            {
                return _ImageDL.GetLastImage(ListingID);
            }

            return null;
        }

        public List<Image> Get(int ListingID)
        {
            S3Helper s3 = new S3Helper();

            List<Image> imageList = _ImageDL.GetImage(ListingID);

            foreach (Image image in imageList)
            {
                image.Src = string.Join("/", new string[] { s3.cloudfront_cdn, image.Src });
            }

            return imageList;
        }

    }
}
