// for user to manage address

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
    public class UserAddressController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // GET: UserAddress
        public ActionResult Index(string keyword = "", int page = 1)
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());

            }

            if (String.IsNullOrEmpty(keyword))
            {
                // set the item number in each page
                int pageItemNum = 10;
                IEnumerable<UserAddress> userAddresses = db.UserAddresses.Include(u => u.User).Where(u => u.UserId == id).OrderByDescending(p => p.CreatingTime).ToList();
                // count the total number of products
                int totalUserAddresses = userAddresses.Count();
                // skip pageItemNum and take the following pageItemNum
                userAddresses = userAddresses.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalUserAddresses)) / (Convert.ToDecimal(pageItemNum)));
                return View(userAddresses);
            }
            else {
                // if keyword is not empty
                // set the item number in each page
                int pageItemNum = 10;

                IEnumerable<UserAddress> userAddresses = db.UserAddresses.Include(u => u.User).Where(u => u.UserId == id).Where(u=>u.Address.Contains(keyword)).OrderByDescending(p => p.CreatingTime).ToList();
                // count the total number of products
                int totalUserAddresses = userAddresses.Count();
                // skip pageItemNum and take the following pageItemNum
                userAddresses = userAddresses.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalUserAddresses)) / (Convert.ToDecimal(pageItemNum)));
                return View(userAddresses);
            }

        }
        

       

        // GET: UserAddress/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: UserAddress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,ReceivingName,Phone,Notes,CreatingTime,UserId")] UserAddress userAddress)
        {
            if (ModelState.IsValid)
            {
                db.UserAddresses.Add(userAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", userAddress.UserId);
            return View(userAddress);
        }

        // GET: UserAddress/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAddress userAddress = db.UserAddresses.Find(id);
            if (userAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", userAddress.UserId);
            return View(userAddress);
        }

        // POST: UserAddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,ReceivingName,Phone,Notes,CreatingTime,UserId")] UserAddress userAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", userAddress.UserId);
            return View(userAddress);
        }

       
        public ActionResult Delete(int id)
        {
            UserAddress userAddress = db.UserAddresses.Find(id);
            db.UserAddresses.Remove(userAddress);
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
