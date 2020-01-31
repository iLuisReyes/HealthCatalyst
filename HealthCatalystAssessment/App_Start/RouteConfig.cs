using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HealthCatalyst.Assessment.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.RouteExistingFiles = true;

            routes.MapRoute(
                name: "staticFileRoute",
                url: "Powershell",
                defaults: new { controller = "Home", action = "DownloadFile" }
            );

            // By default route the user to the Help area if accessing the base URI.
            routes.MapRoute(
                "Help Area",
                "",
                new { controller = "Help", action = "Index" }
            ).DataTokens = new RouteValueDictionary(new { area = "HelpPage" });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Help", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
