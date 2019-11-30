using System.Web.Http;
using System.Web.Mvc;

namespace ExamReg.WebApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            context.Routes.MapHttpRoute(
                name: "DefaultAdminApi",
                routeTemplate: "admin/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
);
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}