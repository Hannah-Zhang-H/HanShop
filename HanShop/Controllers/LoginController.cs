// for admin/staff -> login to the system

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HanShop.Data;
using HanShop.Models;
using Microsoft.Ajax.Utilities;

namespace HanShop.Controllers
{
    public class LoginController : Controller
    {
        private HanShopContext db = new HanShopContext();


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            // 1. check if the username and password is null or not
            if (string.IsNullOrEmpty(username))
            {
                return Content("<script>alert('Username is required'); window.history.back(-1);</script>");
            }

            if (string.IsNullOrEmpty(password))
            {
                return Content("<script>alert('password is required'); window.history.back(-1);</script>");
            }


            var adminInfo = db.Admins.FirstOrDefault(p => p.Name == username && p.Password == password);

            // admin is in charge of staff, admin can create a new staff
            // admin username is "admin", password is "123admin"
            if (username == "admin" && password == "123admin")
            {
                //use Session to remember the admin's info
                Session["currentAdminName"] = "admin";
                Session["currentAdminId"] = "1";
                // if log in successfully, jump to Index method in Admin controller to welcome the admin to log in
                return Redirect("/Admin/Index");
            }
              // if user enters the username and password
            else if (adminInfo != null)
            {
                // and the username and password matches the ones in the db, then this staff logs in successfully
                Session["currentAdminName"] = adminInfo.Name;
                Session["currentAdminId"] = adminInfo.Password;
                //jump to Index method in Admin controller to welcome the staff to log in
                return Redirect("/Admin/Index");
            }
            else
            {
                // otherwise login failed, remind the user.
                return Content("<script>alert('Username or Password is not valid'); window.history.back(-1);</script>");

            }


        }


        // Logout the system
        // if the user clicks the "log off"
        public ActionResult Logout()
        {
            Session["currentAdminName"] = null;
            Session["currentAdminId"] = null;
            return Redirect("/Login/Index");
        }
    }
}