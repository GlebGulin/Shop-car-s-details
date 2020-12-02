
using NGLayer.Models.Data;
using NGLayer.Models.Data.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Binding.Models
{
    public class Db : DbContext
    {
        public DbSet<ShopPages> shoppages { get; set; }
        public DbSet<SideBar> sidebar { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Products> product { get; set; }
        public DbSet<Messangers> messengers { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Reviews> reviews { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<OrdersDetail> ordersdetails { get; set; }
        public DbSet<OrdersAuthorized> ordersauthorizeds { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }

    }
}