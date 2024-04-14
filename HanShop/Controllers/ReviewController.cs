// for admin/staff -> to check order and to check order details

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using HanShop.Data;
using HanShop.Models;

namespace HanShop.Controllers
{
    public class ReviewController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // GET: Review
        // FOR ADMIN
        public ActionResult Index(string keyword= "", int page = 1)
        {
            // set the item number in each page, here i choose 3 items each page
            int pageItemNum = 8;

            IEnumerable<Review> reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).
                Where(r=>r.Product.Name.Contains(keyword)).OrderByDescending(p => p.CreatingTime).ToList();

            // count the total number of products
            int totalReviews = reviews.Count();
            // skip pageItemNum and take the following pageItemNum
            reviews = reviews.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
            // the total page number of all products
            ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalReviews)) / (Convert.ToDecimal(pageItemNum)));

            return View(reviews);
        }




        // FOR USER
        public ActionResult Index2(string keyword = "", int page = 1)
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());
            }


            if (String.IsNullOrEmpty(keyword))
            {
                IEnumerable<Review> reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).
                   Where( p=>p.UserId == id).Where(p => p.User.Id == id).OrderByDescending(p => p.CreatingTime).ToList();

                // set the item number in each page, here i choose 3 items each page
                int pageItemNum = 8;

                // count the total number of products
                int totalReviews = reviews.Count();
                // skip pageItemNum and take the following pageItemNum
                reviews = reviews.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalReviews)) / (Convert.ToDecimal(pageItemNum)));

                return View(reviews);

            }
            else {

                IEnumerable<Review> reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).
                  Where(p => p.Product.Name.Contains(keyword) && p.UserId == id).Where(p => p.User.Id == id).OrderByDescending(p => p.CreatingTime).ToList();

                // set the item number in each page, here i choose 3 items each page
                int pageItemNum = 8;

                // count the total number of products
                int totalReviews = reviews.Count();
                // skip pageItemNum and take the following pageItemNum
                reviews = reviews.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalReviews)) / (Convert.ToDecimal(pageItemNum)));

                return View(reviews);

            }
           
        }





        // In "/Home/Detail", if user logged in and add clicks the submit button, this new review will be created
        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId,CreatingTime,ReviewContent")] Review review)
        {
           db.Reviews.Add(review);
            if (db.SaveChanges() > 0)
            {
                return Content("200");
            }
            else {
                return Content("201");
            }
        }

    

       // Delete
        public ActionResult Delete(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return Content("<script>alert('Deleted successfully!'); window.history.back(-1);</script>");
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
