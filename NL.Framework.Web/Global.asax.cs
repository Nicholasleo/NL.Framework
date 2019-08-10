using Autofac;
using NL.Framework.Web.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NL.Framework.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private  static IContainer _container;

        public static IContainer Container
        {
            get { return _container; }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AutofacConfig.RegisterDependencies();
            AutofacConfig.Register();
        }
    }
}
