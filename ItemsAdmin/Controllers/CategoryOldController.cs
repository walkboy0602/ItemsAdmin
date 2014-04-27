using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MonsterAdmin.Models;
using MonsterAdmin.ViewModel;

namespace MonsterAdmin.Controllers.Shared
{
    public class CategoryController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        // GET: /Category/
        public ActionResult Index(int? ParentID = null)
        {
            var categories = (IEnumerable<RefCategory>)null;

            if (ParentID == null)
            {
                categories = db.RefCategories.Where(x => x.ParentID == null).ToList();
            }
            else
            {
                categories = db.RefCategories
                                .Where(x => x.ParentID == ParentID).ToList();

                ViewBag.ParentName = db.RefCategories.Where(x => x.id == ParentID).Select(x => x.Name).SingleOrDefault();
            }

            return View(categories);
        }

        
        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories);
            return View();
        }


        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RefCategory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.RefCategories.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories, model.ParentID);
            return View(model);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            RefCategory model = db.RefCategories.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories, model.ParentID);
            return View(model);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RefCategory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.Categories = new DropDownViewModel().GetCategories(db.RefCategories, model.ParentID);
            return View(model);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
