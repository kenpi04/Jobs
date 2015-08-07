using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Job.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "HomePage",
               url: "",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Job.Web.Controllers" }
           );
            routes.MapRoute(
              name: "News",
              url: "news",
              defaults: new { controller = "news", action = "index", page = UrlParameter.Optional },
              namespaces: new[] { "Job.Web.Controllers" }
          );
            routes.MapRoute(
          name: "newsdetail",
          url: "news-detail/{id}",
          defaults: new { controller = "news", action = "newsDetail"},
          namespaces: new[] { "Job.Web.Controllers" }
      );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new[]{"Job.Web.Controllers"}
            );
        }
    }
}
