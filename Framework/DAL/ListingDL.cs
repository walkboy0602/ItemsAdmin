using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using System.Data.SqlClient;

namespace Framework.DAL
{
    class ListingDL
    {
        private dbConnection conn;

        //Constructor
        public ListingDL()
        {
            conn = new dbConnection();
        }

        public int Create(int userID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO Listing (CreateBy, CreateDate, LastUpdate, Status) ");
            sb.Append("VALUES (@createBy, @createDate, @lastUpdate, @status) ");

            SqlParameter[] param = 
            {
                new SqlParameter("@createBy", userID),
                new SqlParameter("@createDate", DateTime.Now),
                new SqlParameter("@lastUpdate", DateTime.Now),
                new SqlParameter("@status", (char)ListingStatus.New)
            };

            int ListingID = conn.executeInsertQueryReturnID(sb.ToString(), param);

            return ListingID;
        }

        public int GetLastListing(int userID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT id FROM Listing WHERE CreateBy = @userID AND Status = @status ");

            SqlParameter[] param =
            {
                new SqlParameter("@userID", userID),
                new SqlParameter("@status", (char)ListingStatus.New)
            };

            return conn.executeQueryInt(sb.ToString(), param);
        }

        public bool ValidateListing(int listingID, int userID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT 1 FROM Listing (NOLOCK) WHERE id = @listingID AND CreateBy = @userID AND Status != @status ");

            SqlParameter[] param = 
            {
                new SqlParameter("@listingID", listingID),
                new SqlParameter("@userID", userID),
                new SqlParameter("@status", (char)ListingStatus.Suspended)
            };

            return Convert.ToBoolean(conn.executeQueryInt(sb.ToString(), param));
        }

        public Listing Get(Listing objListing)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT id, SaleType, CategoryType, Title, Description, Price, WarrentyDesc, CreateBy as UserID, CreateDate, CashOnDelivery as COD, OnlinePayment as OnPay, PaymentDescription ");
            sb.Append("FROM Listing (NOLOCK) ");
            sb.Append("WHERE id = @id AND CreateBy = @userID ");

            SqlParameter[] param = {
                                       new SqlParameter("@id", objListing.id),
                                       new SqlParameter("@userID", objListing.UserID)
                                   };

            List<Listing> obj = conn.executeSelectQuery<Listing>(sb.ToString(), param);

            return obj.Count > 0 ? obj[0] : null;

        }

        public bool Save(Listing objListing)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE Listing SET SaleType = @saleType, CategoryType = @categoryType, title = @title, Description = @desc, ");
            sb.Append("WarrentyDesc = @warrentyDesc, PaymentDescription = @paymentDesc, ");
            sb.Append("Price = @price, CashOnDelivery = @cod, OnlinePayment = @onPay, Status = @status, ");
            sb.Append("LastUpdate = @lastUpdate ");
            sb.Append("WHERE id = @listingID ");

            SqlParameter[] param = 
            {
                new SqlParameter("@saleType", objListing.SaleType),
                new SqlParameter("@categoryType", objListing.CategoryType),
                new SqlParameter("@title", objListing.Title),
                new SqlParameter("@desc", objListing.Description),
                new SqlParameter("@warrentyDesc", objListing.WarrentyDesc),
                new SqlParameter("@paymentDesc", objListing.PaymentDescription),
                new SqlParameter("@price", objListing.Price),
                new SqlParameter("@cod", objListing.COD),
                new SqlParameter("@onPay", objListing.OnPay),
                new SqlParameter("@status", (char)ListingStatus.PendingReview),
                new SqlParameter("@lastUpdate", DateTime.Now),
                new SqlParameter("@listingID", objListing.id)
            };

            bool result = conn.executeUpdateQuery(sb.ToString(), param);

            return result;
        }



    }
}
