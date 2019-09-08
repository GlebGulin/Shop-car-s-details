using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<ShopPages> shoppages { get; set; }
        public DbSet<Sidebar> sidebar { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Products> product { get; set; }
    }
}