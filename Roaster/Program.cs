using System;
using NServiceBus;
using NServiceBus.Logging;

namespace Roasting
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Roasting");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            var endpoint = Endpoint.StartAsync(configuration).GetAwaiter().GetResult();

            Console.ReadLine();

            endpoint.StopAsync().GetAwaiter().GetResult();
        }
    }
}
