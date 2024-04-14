// for user to modify name/password/nickname/gender/introduction
// -> click the person icon on the right side of the navbar at the user centre home page
using HanShop.Data;
using HanShop.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HanShop.Controllers
{
    public class UserCentreController : Controller
    {

        private HanShopContext db = new HanShopContext();
        // GET: UserCentre
        // modify user information
        public ActionResult Index()
        {
            int id = 0;
            if (Session["uerId"] != null) { id = int.Parse(Session["uerId"].ToString()); }

           
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return Content("<script>alert('Cannot find the user!'); window.history.back(-1);</script>");
            }
            return View(user);

        }



        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Content("<script>alert('Successfully modified!'); window.history.back(-1);</script>");
            }
            return View(user);
        } 
    }
}