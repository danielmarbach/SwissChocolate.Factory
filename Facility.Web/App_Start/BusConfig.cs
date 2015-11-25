using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Facility.Web.Controllers;
using NServiceBus;
using NServiceBus.Logging;

namespace Facility.Web
{
    public static class BusConfig
    {
        public static IEndpointInstance Start()
        {
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Facility.Web");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            configuration.ExcludeAssemblies("System.Data.SqlServerCe.dll");

            configuration.SendOnly();

            var endpointInstance = Endpoint.Start(configuration).GetAwaiter().GetResult();

            var currentResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(new SimpleTypeResolver(currentResolver, endpointInstance));

            return endpointInstance;
        }

        private class SimpleTypeResolver : IDependencyResolver
        {
            private readonly IDependencyResolver dependencyResolver;
            private readonly IEndpointInstance endpointInstance;

            public SimpleTypeResolver(IDependencyResolver defaultResolver, IEndpointInstance endpointInstance)
            {
                dependencyResolver = defaultResolver;
                this.endpointInstance = endpointInstance;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof (HomeController))
                {
                    return new HomeController(endpointInstance.CreateBusContext());
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