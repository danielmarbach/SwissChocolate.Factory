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

            var bus = Bus.Create(configuration).Start();


            var message = new ProduceChocolateBar { LotNumber = 1111 };
            Console.WriteLine($"Hit enter to produce chocolate bar with lot number {message.LotNumber}");
            Console.ReadLine();
            bus.SendLocal(message);

            message = new ProduceChocolateBar { LotNumber = 2222 };
            Console.WriteLine($"Hit enter to produce chocolate bar with lot number {message.LotNumber}");
            Console.ReadLine();
            bus.SendLocal(message);

            Console.ReadLine();
        }
    }
}
