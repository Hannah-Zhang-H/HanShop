using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HanShop.Data
{
    public class HanShopContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HanShopContext() : base("name=HanShopContext")
        {
        }

        public System.Data.Entity.DbSet<HanShop.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.Review> Reviews { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.UserAddress> UserAddresses { get; set; }
        public System.Data.Entity.DbSet<HanShop.Models.Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<HanShop.Models.OrderDetail> OrderDetails { get; set; }
    }
}
