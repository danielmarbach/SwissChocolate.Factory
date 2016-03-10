using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Facility.Web.Controllers;
using NServiceBus;
using NServiceBus.Logging;

namespace Facility.Web
{
    public static class BusConfig
    {
        public static async Task<IEndpointInstance> Start()
        {
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new EndpointConfiguration();
            configuration.EndpointName("Chocolate.Facility.Web");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            configuration.ExcludeAssemblies("System.Data.SqlServerCe.dll");

            configuration.SendOnly();

            var endpointInstance = await Endpoint.Start(configuration);

            var currentResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(new SimpleTypeResolver(currentResolver, endpointInstance));

            return endpointInstance;
        }

        private class SimpleTypeResolver : IDependencyResolver
        {
            private readonly IDependencyResolver dependencyResolver;
            private readonly IMessageSession messageSession;

            public SimpleTypeResolver(IDependencyResolver defaultResolver, IMessageSession messageSession)
            {
                dependencyResolver = defaultResolver;
                this.messageSession = messageSession;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof (HomeController))
                {
                    return new HomeController(messageSession);
                }
                return dependencyResolver.GetService(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return dependencyResolver.GetServices(serviceType);
            }
        }
    }
}