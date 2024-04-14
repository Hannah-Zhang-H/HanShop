// for admin -> to create/edit/detal/delete a staff

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
    public class AdminsManagementController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // GET: AdminsManagement
        public ActionResult Index(string keyword="", int page = 1)
        {
            // set the item number in each page
            int pageItemNum = 8;

            IEnumerable<Admin> admins = db.Admins.Where(p => p.Name.Contains(keyword)).OrderByDescending(p => p.AddingTime).ToList();

            // count the total number of products
            int totalAdmins = admins.Count();
            // skip pageItemNum and take the following pageItemNum
            admins = admins.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
            // the total page number of all products
            ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalAdmins)) / (Convert.ToDecimal(pageItemNum)));

            return View(admins);
        }

       



        // GET: AdminsManagement/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: AdminsManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Password,Power,AddingTime")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: AdminsManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: AdminsManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Password,Power,AddingTime")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

       


        // Delete
        public ActionResult Delete(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
