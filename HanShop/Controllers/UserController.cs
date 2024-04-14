// for admin/staff to add/edit/detail/delete a user
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
    public class UserController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // GET: User
        // get a parameter to search for a certain user
        public ActionResult Index(string keyword = "" , int page = 1)
        {
            // set the item number in each page
            int pageItemNum = 10;

            // to check if the user information matches any of the keyword content
            IEnumerable<User> users = db.Users.Where(p => p.Username.Contains(keyword)
                                    || p.Nickname.Contains(keyword)
                                    || p.Gender.Contains(keyword)).OrderByDescending(p => p.Id).ToList();



            // count the total number of products
            int totalUsers = users.Count();
            // skip pageItemNum and take the following pageItemNum
            users = users.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
            // the total page number of all products
            ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalUsers)) / (Convert.ToDecimal(pageItemNum)));


            return View(users);

        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Nickname,Gender,Introduction")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Nickname,Gender,Introduction")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

       

        public ActionResult Delete(int id)
        //public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
