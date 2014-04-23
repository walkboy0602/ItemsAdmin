using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using System.Data.SqlClient;

namespace Framework.DAL
{
    class ImageDL : dbConnection
    {
        public bool SaveImage(int ListingID, string FileName, string Src)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO ListingImage (ListingID, FileName, IsCover, CreateDate, Src) ");
            sb.Append("VALUES (@ListingID, @FileName, @IsCover, @CreateDate, @Src) ");

            SqlParameter[] param = {
                                       new SqlParameter("@ListingID", ListingID),
                                       new SqlParameter("@FileName", FileName),
                                       new SqlParameter("@IsCover", false),
                                       new SqlParameter("@CreateDate", DateTime.Now),
                                       new SqlParameter("@Src", Src)
                                   };

            bool result = executeInsertQuery(sb.ToString(), param);

            return result;
        }

        public Image GetLastImage(int ListingID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT TOP 1 id, FileName, IsCover FROM ListingImage (NOLOCK) ");
            sb.Append("WHERE ListingID = @listingID ORDER BY id DESC ");

            SqlParameter[] param = {
                                       new SqlParameter("@listingID", ListingID)
                                   };

            List<Image> image = executeSelectQuery<Image>(sb.ToString(), param);

            return image.Count > 0 ? image[0] : null;

        }

        public List<Image> GetImage(int ListingID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT id, FileName, IsCover, Src FROM ListingImage (NOLOCK) ");
            sb.Append("WHERE ListingID = @listingID ORDER BY id ");

            SqlParameter[] param = {
                                       new SqlParameter("@listingID", ListingID)
                                   };

            return executeSelectQuery<Image>(sb.ToString(), param);
        }

        public bool Delete(int ImageID)
        {
            StringBuilder sb = new StringBuilder();
            bool result = false;

            if (ArchieveImage(ImageID))
            {

                sb.Append("DELETE FROM ListingImage WHERE id = @id ");


                SqlParameter[] param = {
                                     new SqlParameter("@id", ImageID)  
                                   };

                result = executeUpdateQuery(sb.ToString(), param);
            }

            return result;
        }

        public bool ArchieveImage(int ImageID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ListingImageDeleted(id, ListingID, FileName, IsCover, Sort, CreateDate, Src) ");
            sb.Append("SELECT id, ListingID, FileName, IsCover, Sort, CreateDate, Src ");
            sb.Append("FROM ListingImage ");
            sb.Append("WHERE id = @id ");

            SqlParameter[] param = {
                                     new SqlParameter("@id", ImageID)  
                                   };

            return executeUpdateQuery(sb.ToString(), param);
        }
    }
}
