using PeopleProTraining.Controllers;
using PeopleProTraining.Dal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PeopleProTraining
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var tempController = new EmployeesTestController(new PeopleProRepo());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //tempController.Index();

        }
    }
}
