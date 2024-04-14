// for admin/staff -> to check list/edit/details/delete the products

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
using PagedList;

namespace HanShop.Controllers
{
    public class ProductController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // GET: Product
        public ActionResult Index(string keyword = "", int page = 1)
        {

            // set the item number in each page, here i choose 3 items each page
            int pageItemNum = 10;

            if (String.IsNullOrEmpty(keyword))
            {
                IEnumerable<Product> products = db.Products.Include(p => p.Category);

                // count the total number of products
                int totalProducts = products.Count();
                // skip pageItemNum and take the following pageItemNum
                products = products.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalProducts)) / (Convert.ToDecimal(pageItemNum)));


                return View(products.ToList());
            }
            else
            {

                IEnumerable<Product> products = db.Products.Include(p => p.Category).Where(p => p.Name.Contains(keyword));

                // count the total number of products
                int totalProducts = products.Count();
                // skip pageItemNum and take the following pageItemNum
                products = products.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalProducts)) / (Convert.ToDecimal(pageItemNum)));

                return View(products.ToList());
            }

            
        }



        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }




        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,SalePrice,storage,Description,CategoryID,ProductCode,Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }




    // GET: Product/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }





        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,SalePrice,storage,Description,CategoryID,ProductCode,Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }





        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            var cartProduct = db.Carts.Where(p => p.ProductId == id).ToList();
            foreach (var item in cartProduct)
            {
                item.ProductId = null;
            }
            var orderDetailsProduct = db.OrderDetails.Where(p => p.ProductId == id).ToList();
            foreach (var item in orderDetailsProduct)
            {
                item.ProductId = null;
            }


            db.Products.Remove(product);
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
