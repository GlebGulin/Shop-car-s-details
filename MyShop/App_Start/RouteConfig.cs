using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Buy", "Buy/{action}/{body}", new { controller = "Buy", action = "Index", body = UrlParameter.Optional }, new[] { "MyShop.Controllers" });
            //routes.MapRoute(null, "Buy/{action}/{slug}", new { controller = "Buy", action = "Details", slug = UrlParameter.Optional }, new[] { "MyShop.Controllers" });
            routes.MapRoute("OrderCart", "OrderCart/{action}/{id}", new { controller = "OrderCart", action = "Index", id = UrlParameter.Optional }, new[] { "MyShop.Controllers" });




            //routes.MapRoute("Default", "", new { controller = "Buy", action = "Category" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("AllProducts", "Buy/AllProducts", new { controller = "Buy", action = "AllProducts" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("ClientReviewPartial", "Buy/ClientReviewPartial", new { controller = "Buy", action = "ClientReviewPartial" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("OrderCartPartial", "OrderCart/OrderCartPartial", new { controller = "OrderCart", action = "OrderCartPartial" }, new[] { "MyShop.Controllers" });

            routes.MapRoute("PageMenuPatial", "Page/PageMenuPatial", new { controller = "Page", action = "PageMenuPatial" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("ClientCommentPartial", "Page/ClientCommentPartial", new { controller = "Page", action = "ClientCommentPartial" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("SidebarPartial", "Page/SidebarPartial", new { controller = "Page", action = "SidebarPartial" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("CategoryMenuPartial", "Buy/CategoryMenuPartial", new { controller = "Buy", action = "CategoryMenuPartial" }, new[] { "MyShop.Controllers" });
            routes.MapRoute("Page", "{page}", new { controller = "Page", action = "Index" }, new[] { "MyShop.Controllers" });

            routes.MapRoute("Default", "", new { controller = "Page", action = "Index" }, new[] { "MyShop.Controllers" });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
