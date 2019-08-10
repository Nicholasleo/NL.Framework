using Autofac;
using Autofac.Integration.Mvc;
using NL.Framework.DAL;
using NL.Framework.IDAL;
using NL.Framework.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
