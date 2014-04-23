using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DAL;
using Framework.Model;

namespace Framework.BLL
{
    public class ListingBL
    {
        ListingDL _ListingDL;

        public ListingBL()
        {
            _ListingDL = new ListingDL();
        }

        public int Create(int UserID)
        {
            int ListingID = _ListingDL.GetLastListing(UserID);

            if (ListingID == 0)
            {
                ListingID = _ListingDL.Create(UserID);
            }

            return ListingID;
        }

        public Listing Get(Listing objListing)
        {
            return _ListingDL.Get(objListing);
        }

        public bool Save(Listing objListing)
        {
            return _ListingDL.Save(objListing);
        }

        public bool ValidateListing(int listingID, int UserID)
        {
            return _ListingDL.ValidateListing(listingID, UserID);
        }

    }
}
