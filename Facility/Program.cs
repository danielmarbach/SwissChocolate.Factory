using System;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Facility
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Facility");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            var bus = Endpoint.StartAsync(configuration).GetAwaiter().GetResult().CreateBusContext();

            Console.ReadLine();
        }
    }
}
