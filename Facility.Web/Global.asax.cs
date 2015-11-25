using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NServiceBus;

namespace Facility.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ISendOnlyBus bus;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DatabaseConfig.CreateDatabase();

            bus = BusConfig.Start();
        }

        public override void Dispose()
        {
            bus.Dispose();

            base.Dispose();
        }
    }
}
