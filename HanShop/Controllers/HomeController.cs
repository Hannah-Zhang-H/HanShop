// this HomeContorller has varous methods.
// a nav item for user to log in/ sign up/log off
// a nav itemfor user to add products into shopping cart, display the number of the products on top of teh cart
// a nav item for staff/admin to login
// a searching bar for searching goods
// Homepage item on the left side of the nav -> Han'sShoppingCentre

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using HanShop.Data;
using HanShop.Models;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace HanShop.Controllers
{
    public class HomeController : Controller
    {
        private HanShopContext db = new HanShopContext();
        public ActionResult Index()
        {
            var productList = db.Products.OrderBy(p => p.Name).ToList();

            // save the current URL to Session
            // just incase the user will add the products to cart before logging in
            Session["ReturnUrl"] = Request.Url.ToString();

            return View(productList);

        }




        // user centre view
        public ActionResult UserCentre()
        {
            return View();

        }









        public ActionResult List(string category, int? CategoryId, string keyword, string sort, int page = 1)
        {
            int pageItemNum = 6; // item in each page
            // create query
            var query = db.Products.AsQueryable();

            if (!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword)
                    || p.Description.Contains(keyword)
                    || p.Category.Name.Contains(keyword));
                ViewBag.keyword = keyword;
            }

            // if category has value
            if (!String.IsNullOrEmpty(category))
            {
                // if category == All, and keyword is empty
                if (category == "All" && (String.IsNullOrEmpty(keyword)))
                {
                    // display all products to user
                    query = db.Products.AsQueryable();
                }
                // if category is not "All"
                else if (category != "All")
                {
                    // display the product which has the same category
                    query = query.Where(p => p.Category.Name == category);
                }


                // if category is All, and at the sametime the categoryID has value and the value is not 0,
                // then will set the category to null, and find the products which satisfys te categoryID
                if (category == "All" && ((CategoryId.HasValue) && (CategoryId != 0)))
                {
                    category = null;
                    query = query.Where(p => p.CategoryID == CategoryId.Value);

                }

            }
            // if category is null, then just focus on categoryID
            else if (CategoryId.HasValue && (CategoryId != 0))
            {
                query = query.Where(p => p.CategoryID == CategoryId.Value);
            }


            // check if sort value is null or not
            if (String.IsNullOrEmpty(sort))
            {
                // if null, then display the list by product name
                query = query.OrderBy(p => p.Category.Name);
            }
            else
            {
                // if user select price low to high:
                if (sort == "price_asc")
                {
                    query = query.OrderBy(p => p.SalePrice);
                }
                // if user select price high to low:
                else if (sort == "price_desc")
                {
                    query = query.OrderByDescending(p => p.SalePrice);
                }
            }







            // calculate the total products
            int totalProducts = query.Count();
            var products = query.Skip((page - 1) * pageItemNum).Take(pageItemNum).ToList();




            var categories = db.Products.GroupBy(p => p.Category.Name).Select(g => g.Key).ToList();

            ViewBag.Category = new SelectList(categories);



            // set the pagination 
            ViewBag.pageNum = Math.Ceiling((decimal)totalProducts / pageItemNum);
            ViewBag.currentPage = page;

            // save the current URL to Session, just incase the user login.
            // if the user logs in, the user can still come back to this page.
            Session["ReturnUrl"] = Request.Url.ToString();




            return View(products);
        }









        public ActionResult Detail(int? id)
        {
            // if can not find the certain prodcut, then tell the user the product can not be found
            if (id == null)
            {
                return Content("<script>alert('Can not find the product.'); window.history.back(-1);</script>");
            }
            Product product = db.Products.FirstOrDefault(p => p.ID == id);

            if (product == null)
            {
                return Content("<script>alert('Can not find the product.'); window.history.back(-1);</script>");

            }

            // save the current URL to Session
            // just incase the user will add the products to cart before logging in
            Session["ReturnUrl"] = Request.Url.ToString();



            // find all the reviews of this product
            ViewBag.review = db.Reviews.Where(p => p.ProductId == id).ToList();




            // check if user id exists,if yes, get the user ID
            int uid = 0;
            if (Session["uerId"] != null)
            {
                uid = int.Parse(Session["uerId"].ToString());

            }


            // check if the user has bought this certain product, if yes, then the user can add a review,
            // otherwise, user can see notice to purchase the product first.
            var hasBought = db.OrderDetails.FirstOrDefault(p => p.ProductId == id &&p.Order.User.Id == uid);
            if (hasBought != null)
            {
                // this viewBag will be used in "/Home/Detail" -> review part 
                ViewBag.hasBought = true.ToString();

            }
            



                return View(product);
        }


        public ActionResult Login()
        {

            return View();
        }





        // user log in twards the form
        [HttpPost]
        public ActionResult Login(User u)
        {
            var userInfo = db.Users.FirstOrDefault(p => p.Username == u.Username);
            if (userInfo != null)
            {
                // check if password mathches
                if (userInfo.Password != u.Password)
                {
                    return Content("<script>alert('Password error'); window.history.back(-1);</script>");
                }
                else
                {
                    // if password mathces, jump to home page
                    Session["userNickname"] = userInfo.Nickname;
                    Session["uerId"] = userInfo.Id;





                    // save it to Sesson-> whenever the usr logs in, the total products number will be displayed at the side of the cart
                    // the number will get changed when the user add product
                    var totalNum = 0;
                    var orders = db.Carts.Where(p => p.User.Id == userInfo.Id).ToList();
                    foreach (var item in orders)
                    {
                        totalNum += item.ProductNum;
                    }

                    Session["productNum"] = totalNum;




                    // code here is prepar for CART log in part.
                    // get the former page URL
                    // please check Detail function in this page, i store the URL there
                    string returnUrl = Session["ReturnUrl"] as string;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        Session.Remove("ReturnUrl");
                        return Redirect(returnUrl);
                    }


                    

                    return Redirect("/Home/Index");

                }
            }
            else
            {
                return Content("<script>alert('User does not exists! Please sign up first'); window.history.back(-1);</script>");

            }
        }


        // user log out 
        public ActionResult Logout()
        {
            Session["userNickname"] = null;
            Session["uerId"] = null;


            // get the former page URL
            // if found the former url before logout, then will jump to that url after logout
            string returnUrl = Session["ReturnUrl"] as string;
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Session.Remove("ReturnUrl");
                return Redirect(returnUrl);
            }

            // if cannot find any former url, then jump back to /home/index
            return View("/Home/Index");
        }






        public ActionResult Signup()
        {

            return View();

        }


        //new user sign up twards the form
        [HttpPost]
        public ActionResult Signup(User u, string passwordAgain)
        {
            if (ModelState.IsValid)
            {
                // check if the password the user typed in twice got the same content
                if (u.Password != passwordAgain)
                {
                    ModelState.AddModelError("passwordAgain", "The password confirmation does not match the password.");
                    return View(u);
                }

                // if yes, go ahead to sign up this new user

                var userInfo = db.Users.FirstOrDefault(p => p.Username == u.Username);
                if (userInfo != null)
                {
                    return Content("<script>alert('User exists! Please sign in'); window.history.back(-1);</script>");
                }
                db.Users.Add(u);
                db.SaveChanges();


                //ViewBag.successSignup = "Contratulations, you have successfully signed up, enjoy your shopping.";

                // get the former page URL
                // if found the former url before logout, then will jump to that url after logout
                string returnUrl = Session["ReturnUrl"] as string;
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    ViewBag.ReturnUrl = returnUrl;
                    Session.Remove("ReturnUrl");
                    //return Redirect(returnUrl);

                    return Content("<script>alert('Congratulations, you have successfully signed up, enjoy your shopping'); window.history.back(-1);</script>");
                }

                // if cannot find any former url, then jump back to /home/index
                return Content("<script>alert('Congratulations, you have successfully signed up, enjoy your shopping'); window.history.back(-1);</script>");
             


            }

            return View(u);



        }





        // AddToCart function
        [HttpPost]
        public ActionResult AddToCart(int productId, int productNum)
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());

            }

            // check if the product has already exists, if yes, just accumulate the number of the product, otherwise add the new product
            Cart info = db.Carts.FirstOrDefault(p => p.ProductId == productId && p.UserId == id);
            if (info != null)
            {
                info.ProductNum += productNum;
                // save it to db
                db.Entry(info).State = EntityState.Modified;

            }
            else
            {

                Cart cart = new Cart()
                {
                    ProductId = productId,
                    ProductNum = productNum,
                    CreatingTime = DateTime.Now,
                    UserId = id

                };

                db.Carts.Add(cart);

            }

            if (db.SaveChanges() > 0)
            {
                // save it to Sesson
                // so the total number in shopping cart will get changed whenever the user adds new products into a cart
                var totalNum = 0;
                var orders = db.Carts.Where(p => p.User.Id == id).ToList();
                foreach (var item in orders)
                {
                    totalNum += item.ProductNum;
                }

                Session["productNum"] = totalNum;

                return Content("200");
            }
            else
            {
                return Content("201");
            }
        }



        // if user has already signed in
        // click Cart on the nav bar ,then go to cart list
        // if the user has not signed in, then will remind the user to sign in first.
        public ActionResult Cart()
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());
            }
            else
            {
                return Content("<script>alert('Please sign in to check your cart!'); window.history.back(-1);</script>");
            }



            // get the list, the list got user, the list got products, and the userid matches the id
            var cartList = db.Carts.Include(u => u.User).Include(u => u.Product).Where(u => u.UserId == id).ToList();
            return View(cartList);
        }


        // delete a certain product from a cart
        [HttpPost]
        public ActionResult CartDelProduct(int productId)
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());
            }

            var info = db.Carts.FirstOrDefault(p => p.ProductId == productId && p.UserId == id);
            db.Carts.Remove(info);

            if (db.SaveChanges() > 0)
            {
                // whenever the user delets a product, the numbers in the cart will be decreased as well. 
                // so the number displayed at the right side of the cart will get changed as well.
                var totalNum = 0;
                var orders = db.Carts.Where(p => p.User.Id == id).ToList();
                foreach (var item in orders)
                {
                    totalNum += item.ProductNum;
                }

                Session["productNum"] = totalNum;


                return Content("200");
            }
            else { return Content("201"); }

        }




        // pay 
        public ActionResult Pay()
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());
            }
            else
            {
                return Content("<script>alert('Please sign in to check your cart!'); window.history.back(-1);</script>");
            }

            // get the list, the list got user, the list got products, and the userid matches the id
            var cartList = db.Carts.Include(u => u.User).Include(u => u.Product).Where(u => u.UserId == id).ToList();

            // get address information
            ViewBag.address = db.UserAddresses.Where(u => u.UserId == id).ToList();
            return View(cartList);

        }


        [HttpPost]
        public ActionResult Pay(int addressId, string paymentMethod, string notes)
        {
            int id = 0;
            if (Session["uerId"] != null)
            {
                id = int.Parse(Session["uerId"].ToString());
            }

            // get the certain user's products in his/her cart
            var productsInCart = db.Carts.Include(p => p.User).Include(p => p.Product).Where(p => p.UserId == id).ToList();

            var sumPrice = db.Carts.Where(p => p.UserId == id).Sum(p => p.Product.SalePrice);

            // order
            Order order = new Order()
            {
                UserId = id,
                OrderNumber = "order" + DateTime.Now.ToString("yyMMddssHH"),
                SumPrice = (decimal)sumPrice,
                Notes = notes,
                CreatingTime = DateTime.Now,
                IsPaid = true,
                OrderState = 0, //haven't pakged yet
                PaymentMethod = paymentMethod,
            };
            db.Orders.Add(order);

            // order detail

            foreach (var item in productsInCart)
            {
                OrderDetail detail = new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Count = item.ProductNum,
                    Price = (decimal)item.Product.SalePrice,
                    SumPrice = item.ProductNum * (decimal)item.Product.SalePrice,
                    Title = item.Product.Name
                };

                // save all data to orderDetail
                db.OrderDetails.Add(detail);


                // need to empty the cart
                db.Carts.Remove(item);
                // the cart product number will be displayed as 0, because no items in the cart now.
                Session["productNum"] = 0;
            }

            if (db.SaveChanges() > 0) {

                return Content("200");
            }
            else { return Content("201"); }

        }






        public ActionResult Findmore()
        {
            return View();

        }







    }




}