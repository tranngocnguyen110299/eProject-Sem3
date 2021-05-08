using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineFloralDelivery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            ///

            routes.MapRoute(
                name: "Bouquet Detail",
                url: "bouquet-{id}",
                defaults: new { controller = "BouquetDetail", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineFloralDelivery.Controllers" }
            );

            routes.MapRoute(
                name: "Add To Cart",
                url: "add-to-cart",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineFloralDelivery.Controllers" }
            );
            routes.MapRoute(
                name: "Meaning",
                url: "Meaning",
                defaults: new { controller = "Flower", action = "Meaning", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineFloralDelivery.Controllers" }
            );
            routes.MapRoute(
                name: "Occasion",
                url: "Occasion",
                defaults: new { controller = "Bouquet", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineFloralDelivery.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "cart",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineFloralDelivery.Controllers" }
            );
            routes.MapRoute(
                name: "Blog Category",
                url: "blogcategory-{id}",
                defaults: new { controller = "Blog", action = "BlogList", id = UrlParameter.Optional },
                namespaces: new[] { "Azure_Assignment.Controllers" }
            );

            routes.MapRoute(
                name: "Blog Detail",
                url: "blog-{id}",
                defaults: new { controller = "Blog", action = "BlogDetail", id = UrlParameter.Optional },
                namespaces: new[] { "Azure_Assignment.Controllers" }
            );



            ///
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineFloralDelivery.Controllers" }
            );

            
        }
    }
}
