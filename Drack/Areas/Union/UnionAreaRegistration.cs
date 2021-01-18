using System.Web.Mvc;

namespace Drack.Areas.Union
{
    public class UnionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Union";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Union_default",
                "Union/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new[] { "Drack.Areas.Union.Controllers" }
            );
        }
    }
}