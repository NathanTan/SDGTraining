﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PeopleProTraining
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            /*
        routes.MapRoute(
            name: "Hello",
            url: "{controller}/{action}",
           new { controller = "EmployeeTest", action = "Welcome" }
            );
                */
            routes.MapRoute( 
                name: "EmployeesTest",
                url: "EmployeesTest/Index", 
                defaults: new { controller = "EmployeesTest", action = "Index" }
);
        /*
            routes.MapRoute(       
                name: "Employees",   
                url: "{controller}/{action}",   
                defaults: new { controller = "Employees", action = "Index" }

);

            routes.MapRoute(       
                name: "Department",       
                url: "{controller}/{action}",   
                defaults: new { controller = "Department", action = "Index" }
);

            routes.MapRoute(  
                name: "Building",
                url: "{controller}/{action}", 
                defaults: new { controller = "Building", action = "Index" }
);               */
            routes.MapRoute(
                name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);

        


        }

    }
}
