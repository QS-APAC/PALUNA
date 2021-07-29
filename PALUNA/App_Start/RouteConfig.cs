using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PALUNA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "PALUNA.Controllers" }

            );

            routes.MapRoute(
                name: "TypeRoute",
                url: "{controller}/Type/{id}",
                defaults: new { controller = "Home", action = "Type", id = UrlParameter.Optional },
                new[] { "PALUNA.Controllers" }

            );

        }

    }
}
