using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Core.Data;
using App.Core.Services;

namespace App.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        private ShopDBEntities db = new ShopDBEntities();

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        // GET: /Category/
        public ActionResult Index(int? ParentID = null)
        {
            var categories = _categoryService.GetCategoryByParent(ParentID);

            if (ParentID != null)
            {
                ViewBag.ParentName = _categoryService.GetParentName(ParentID);
            }
            return View(categories);
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefCategory refcategory = db.RefCategories.Find(id);
            if (refcategory == null)
            {
                return HttpNotFound();
            }
            return View(refcategory);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,Name,ParentID,MetaDescription,MetaKeyword,Description,Sort,isActive")] RefCategory refcategory)
        {
            if (ModelState.IsValid)
            {
                db.RefCategories.Add(refcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refcategory);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefCategory refcategory = db.RefCategories.Find(id);
            if (refcategory == null)
            {
                return HttpNotFound();
            }
            return View(refcategory);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,Name,ParentID,MetaDescription,MetaKeyword,Description,Sort,isActive")] RefCategory refcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refcategory);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefCategory refcategory = db.RefCategories.Find(id);
            if (refcategory == null)
            {
                return HttpNotFound();
            }
            return View(refcategory);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefCategory refcategory = db.RefCategories.Find(id);
            db.RefCategories.Remove(refcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
