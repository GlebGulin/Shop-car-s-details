using Binding.Models;
using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest()
        {
            if (User == null)
            {
                return;
            }
            string username = Context.User.Identity.Name;
            string[] userroles = null;
            using (Db db = new Db())
            {
                Users users = db.users.FirstOrDefault(x => x.Mail.Equals(username));
                userroles = db.userRoles.Where(x => x.UserId == users.Id).Select(x => x.Role.NameRole).ToArray();
            }
            IIdentity userIdentity = new GenericIdentity(username);
            IPrincipal newObj = new GenericPrincipal(userIdentity, userroles);
            Context.User = newObj;
        }
    }
}
