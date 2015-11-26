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
        public static ISendOnlyBus Start()
        {
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Facility.Web");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            var bus = Bus.CreateSendOnly(configuration);

            var currentResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(new SimpleTypeResolver(currentResolver, bus));

            return bus;
        }

        private class SimpleTypeResolver : IDependencyResolver
        {
            private readonly IDependencyResolver dependencyResolver;
            private readonly ISendOnlyBus bus;

            public SimpleTypeResolver(IDependencyResolver defaultResolver, ISendOnlyBus bus)
            {
                dependencyResolver = defaultResolver;
                this.bus = bus;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof (HomeController))
                {
                    return new HomeController(bus);
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