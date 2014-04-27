using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Core.Data;
using App.Core.Models;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace App.Core.Services
{
    public interface IListingService
    {
        void SaveSpecifications(List<ListingSpec> listingSpecs);
    }

    public class ListingService
    {
        private readonly ShopDBEntities db = new ShopDBEntities();

        public void SaveSpecification(List<ListingSpec> listingSpecs)
        {
            int listingID = listingSpecs[0].ListingID;

            //Remove everything
            db.ListingSpecs.RemoveRange(db.ListingSpecs
                                        .Where(d => d.ListingID == listingID));

            //Add
            db.ListingSpecs.AddRange(listingSpecs);

            db.SaveChanges();
        }
    }
}
