// for admin/staff -> to check order and to check order details
// for user -> to check order and to check order details. create order is in HomeController.


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
    public class OrderController : Controller
    {
        private HanShopContext db = new HanShopContext();

        // ADMIN to manage order
        // GET: Order list
        public ActionResult Index(string keyword = "", int page = 1)
        {
            // set the item number in each page, here i choose 3 items each page
            int pageItemNum = 5;


            // according to user's nickname or name
            IEnumerable<Order> orders = db.Orders.Include(o => o.User).Where(p => p.OrderNumber.Contains(keyword)).OrderByDescending(p=>p.CreatingTime).ToList();
            // count the total number of products
            int totalOrders = orders.Count();
            // skip pageItemNum and take the following pageItemNum
            orders = orders.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
            // the total page number of all products
            ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalOrders)) / (Convert.ToDecimal(pageItemNum)));

            return View(orders);
        }



        // USER to manage order 
        public ActionResult Index2(string keyword = "", int page = 1)
        {
            // set the item number in each page, here i choose 3 items each page
            int pageItemNum = 5;


            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());
            }
            // according to user's exact order number to find the certain order
            if (String.IsNullOrEmpty(keyword))
            {
                IEnumerable<Order> orders = db.Orders.Include(o => o.User).Where(u => u.UserId == id).OrderByDescending(p => p.CreatingTime).ToList();

                // count the total number of products
                int totalOrders = orders.Count();
                // skip pageItemNum and take the following pageItemNum
                orders = orders.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalOrders)) / (Convert.ToDecimal(pageItemNum)));


                return View(orders);
            }
            else
            {

                // if key word is not empty, just check if the order number contans the keyword and display the relate orders to user
                IEnumerable<Order> orders = db.Orders.Include(o => o.User).Where(u => u.UserId == id && u.OrderNumber.Contains(keyword)).ToList();


                // count the total number of products
                int totalOrders = orders.Count();
                // skip pageItemNum and take the following pageItemNum
                orders = orders.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();
                // the total page number of all products
                ViewBag.pageNum = Math.Ceiling((Convert.ToDecimal(totalOrders)) / (Convert.ToDecimal(pageItemNum)));


                return View(orders);


            }
        }














        // for ADMIN
        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Content("<script>alert('bad request'); window.history.back(-1);</script>");
            }
            ViewBag.order = db.Orders.Find(id);

            // order detail
            var list = db.OrderDetails.Where(p => p.OrderId == id).ToList();
            return View(list);
        }



        // for USER
        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.order = db.Orders.Find(id);

            // order detail
            var list = db.OrderDetails.Where(p => p.OrderId == id).ToList();
            return View(list);
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
