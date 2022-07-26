using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentManagementWebApp
{
    public class RouteConfig
    {
        /// <summary>
        /// Register Routes into Route Table
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional 
                }
            );

            routes.MapRoute(
                name: "Student",
                url: "students/{id}",
                defaults: new 
                { 
                    controller = "Student", 
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
