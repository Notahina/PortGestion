using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PortGestion
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "insertprevision",
                url: "{controller}/{action}",
                defaults: new { controller = "Prevision", action = "insert" }
            );
            routes.MapRoute(
                name: "insertpropostion",
                url: "{controller}/{action}",
                defaults: new { controller = "Prevision", action = "propos" }
            );
            routes.MapRoute(
                name: "insertevenement",
                url: "{controller}/{action}",
                defaults: new { controller = "Evenement", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
