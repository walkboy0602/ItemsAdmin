using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using MonsterAdmin.Models;
using MonsterAdmin.ViewModel;
using System.Net;
using Newtonsoft.Json;
using Framework;

namespace MonsterAdmin.Controllers.merchant
{
    public class ListingController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        //
        // GET: /Listing/

        public ActionResult Index()
        {
            var listings = db.Listings.Include(l => l.RefCategory);

            return View(listings.ToList());
        }

        public ActionResult Create2()
        {
            return View();
        }

        // GET: /Listing/Create

        public ActionResult Create()
        {
            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories);
            ViewBag.Currencies = new DropDownViewModel().CurrencyList();

            return View();
        }

        //
        // POST: /Listing/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Listing listing)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Listings.Add(listing);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories);
            ViewBag.Currencies = new DropDownViewModel().CurrencyList();
            return View(listing);
        }

        //
        // GET: /Listing/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = listing.id;
            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories);
            ViewBag.Currencies = new DropDownViewModel().CurrencyList(listing.Currency.TrimEnd());
            return View(listing);
        }

        //
        // POST: /Listing/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Listing listing)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(listing).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories);
            ViewBag.Currencies = new DropDownViewModel().CurrencyList();

            return View(listing);
        }

        #region Image

        //
        // GET: /Listing/GetImage/5

        public ActionResult Image(int id = 0)
        {
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = listing.id;
            return View(listing);
        }

        // AJAX
        // GET: /Listing/GetImage/5

        [HttpGet, ActionName("GetImage")]
        public JsonResult GetImage(int id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<ListingImage> listingImage = db.ListingImages
                                                .Where(l => l.ListingID == id).ToList();

            if (listingImage == null)
            {
                return Json(false);
            }

            string json = JsonConvert.SerializeObject(listingImage, Formatting.Indented);

            return Json(listingImage, JsonRequestBehavior.AllowGet);

        }

        // AJAX
        // POST: /Listing/ImageUpload

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ImageUpload(Listing listing)
        {
            ListingImage listingImage = new ListingImage();

            S3Helper s3 = new S3Helper();

            DateTime create_date = (DateTime)listing.CreateDate;

            string YearMonthDay = string.Empty;

            YearMonthDay = create_date.Year.ToString() + create_date.Month.ToString("d2") + create_date.Day.ToString();

            var image = WebImage.GetImageFromRequest();
            string hashName = s3.Hash(image.FileName, listing.id);

            //file format
            //env + listing + yyyymmdd + listingid + size

            string imageURL = string.Join("/", new string[] { s3.env, "listing", YearMonthDay, listing.id.ToString(), "####size####", hashName });

            bool status = false;

            byte[] fileBytes = image.GetBytes();

            //Thumnbnail
            byte[] s0 = s3.CreateImage(fileBytes, image.FileName, 56, 42);
            status = s3.UploadToS3(s0, imageURL.Replace("####size####", "s0"));

            //Standard
            byte[] s1 = s3.CreateImage(fileBytes, image.FileName, 315, 230);
            status = s3.UploadToS3(s1, imageURL.Replace("####size####", "s1"));

            //Large
            byte[] s2 = s3.CreateImage(fileBytes, image.FileName, 640, 480);
            status = s3.UploadToS3(s2, imageURL.Replace("####size####", "s2"));

            listingImage.ListingID = listing.id;
            listingImage.FileName = hashName;
            listingImage.Description = image.FileName.Substring(0, image.FileName.IndexOf(".")).Truncate(30);
            listingImage.Src = imageURL;
            listingImage.CreateDate = DateTime.Now;

            db.ListingImages.Add(listingImage);
            db.SaveChanges();

            return Json(listingImage);
        }

        [HttpPost]
        public JsonResult ChangeDesc(ListingImage listingImage)
        {
            if (listingImage != null)
            {
                db.Entry(listingImage).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(listingImage);
        }

        [HttpPost]
        public JsonResult DeleteImage(ListingImage listingImage)
        {
            if (listingImage != null)
            {
                S3Helper s3 = new S3Helper();

                s3.DeletePhoto(listingImage.Src.Replace("####size####", "s0"));
                s3.DeletePhoto(listingImage.Src.Replace("####size####", "s1"));
                s3.DeletePhoto(listingImage.Src.Replace("####size####", "s2"));

                var model = db.ListingImages.Find(listingImage.id);

                db.ListingImages.Remove(model);
                db.SaveChanges();

            }

            return Json(true);
        }

        #endregion

        #region Specification

        public ActionResult Specification(int id = 0)
        {
            Listing listing = db.Listings.Find(id);

            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = listing.id;
            return View(listing);
        }

        public JsonResult SaveSpecification(List<ListingSpec> listingSpec)
        {
            if (listingSpec != null)
            {
                int listingID = listingSpec[0].ListingID;

                //Remove everything
                db.ListingSpecs.RemoveRange(db.ListingSpecs
                                            .Where(d => d.ListingID == listingID));

                //Add
                db.ListingSpecs.AddRange(listingSpec);

                db.SaveChanges();

            }

            return Json(true);
        }

        // AJAX
        // GET: /Listing/GetSpecification/5

        [HttpGet, ActionName("GetSpecification")]
        public JsonResult GetSpecification(int ListingID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<ListingSpec> listingSpecs = db.ListingSpecs
                                                .Where(l => l.ListingID == ListingID).ToList();

            if (listingSpecs == null)
            {
                return Json(false);
            }

            string json = JsonConvert.SerializeObject(listingSpecs, Formatting.Indented);

            return Json(listingSpecs, JsonRequestBehavior.AllowGet);
        }

        #endregion



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}