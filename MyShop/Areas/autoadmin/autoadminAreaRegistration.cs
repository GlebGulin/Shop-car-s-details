using System.Web.Mvc;

namespace MyShop.Areas.autoadmin
{
    public class autoadminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "autoadmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "autoadmin_default",
                "autoadmin/{controller}/{action}/{id}",
                new { controller = "Panel", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}