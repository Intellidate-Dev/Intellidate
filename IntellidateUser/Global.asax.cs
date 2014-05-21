using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace IntellidateUser
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                                name: "API Default",
                                routeTemplate: "api/{controller}/{id}",
                                defaults: new { id = RouteParameter.Optional }
                            );
            // Routing
            RouteTable.Routes.MapPageRoute("RegisterRoute", "{Name}", "~/{Name}.aspx");
            RouteTable.Routes.MapPageRoute("jsBackendRoute", "js/{Name}", "~/js/{Name}.aspx");
            RouteTable.Routes.MapPageRoute("LoggedinRoute", "web/{Name}", "~/web/{Name}.aspx");
        }
    }
}