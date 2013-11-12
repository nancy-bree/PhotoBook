using PhotoBook.DAL;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebMatrix.WebData;
using PhotoBook.Infrastructure;

namespace PhotoBook
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            PhotoBookContext context = new PhotoBookContext();
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("PhotoBookContext",
                    "Users", "ID", "UserName", autoCreateTables: true);

            ControllerBuilder.Current.SetControllerFactory(new PhotoBookControllerFactory());
            BootStrapper.ConfigureDependencies();
        }
    }
}