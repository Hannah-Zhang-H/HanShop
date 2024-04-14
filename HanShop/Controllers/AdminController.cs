// for admin/staff -> welcome admin/staff to log in the system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HanShop.Models;
using Microsoft.Ajax.Utilities;

namespace HanShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}