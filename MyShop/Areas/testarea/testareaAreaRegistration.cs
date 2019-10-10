using System.Web.Mvc;

namespace MyShop.Areas.testarea
{
    public class testareaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "testarea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "testarea_default",
                "testarea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}