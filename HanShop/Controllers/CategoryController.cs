    // for admin/staff -> to create/delete a category

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HanShop.Data;
using HanShop.Models;

namespace HanShop.Controllers
{
    public class CategoryController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // GET: Category
        public ActionResult Index(string keyword="", int page = 1)
        {

            // set the item number in each page
            int pageItemNum = 10;

            // search for a certain category by typing in keyword
            IEnumerable<Category> ctegories = db.Categories.Where(p => p.Name.Contains(keyword)).ToList();

            // count the total number of products
            int totalCtegories = ctegories.Count();
            // skip pageItemNum and take the following pageItemNum
            ctegories = ctegories.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
            // the total page number of all products
            ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalCtegories)) / (Convert.ToDecimal(pageItemNum)));


            return View(ctegories);
        }

      





        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }




        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }




        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }



        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }





        // Delete Category/Delete/5
   
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            foreach(var item in category.Products)
            {
                item.CategoryID = null;
            }
            db.Categories.Remove(category);
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
