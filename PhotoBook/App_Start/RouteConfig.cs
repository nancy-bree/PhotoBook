using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoBook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            //routes.MapRoute(
            //    name: "17",
            //    url: "AccessDenied",
            //    defaults: new { controller = "Error", action = "Http403" }
            //);

            //routes.MapRoute(
            //    name: "16",
            //    url: "NotFound",
            //    defaults: new { controller = "Error", action = "Http404" }
            //);

            //routes.MapRoute(
            //    name: "15",
            //    url: "Failure",
            //    defaults: new { controller = "Account", action = "ConfirmationFailure" }
            //);

            //routes.MapRoute(
            //    name: "14",
            //    url: "Success",
            //    defaults: new { controller = "Account", action = "ConfirmationSuccess" }
            //);

            //routes.MapRoute(
            //    name: "13",
            //    url: "StepTwo",
            //    defaults: new { controller = "Account", action = "RegisterStepTwo", }
            //);

            //routes.MapRoute(
            //    name: "12",
            //    url: "Login",
            //    defaults: new { controller = "Account", action = "Login" }
            //);

            //routes.MapRoute(
            //    name: "11",
            //    url: "Register",
            //    defaults: new { controller = "Account", action = "Register" }
            //);

            //routes.MapRoute(
            //    name: "10",
            //    url: "User",
            //    defaults: new { controller = "Account", action = "Manage", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "9",
            //    url: "Logoff",
            //    defaults: new { controller = "Account", action = "LogOff" }
            //);

            //routes.MapRoute(
            //    name: "8",
            //    url: "Tag/{id}",
            //    defaults: new { controller = "Tag", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "7",
            //    url: "Delete/{id}",
            //    defaults: new { controller = "Photo", action = "Delete", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "6",
            //    url: "Edit/{id}",
            //    defaults: new { controller = "Photo", action = "Edit", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "5",
            //    url: "Photo/{id}",
            //    defaults: new { controller = "Photo", action = "Details", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "4",
            //    url: "Slideshow/{id}",
            //    defaults: new { controller = "Photo", action = "Slideshow", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "3",
            //    url: "Upload",
            //    defaults: new { controller = "Photo", action = "Upload" }
            //);

            //routes.MapRoute(
            //    name: "2",
            //    url: "Album/{id}",
            //    defaults: new { controller = "PhotoBook", action = "Photos", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "1",
            //    url: "Albums",
            //    defaults: new { controller = "PhotoBook", action = "UserAlbum" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PhotoBook", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}