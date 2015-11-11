using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using NServiceBus;
using NServiceBus.Logging;

namespace Blending
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = new ServiceHost(typeof (VanillaService));
            serviceHost.AddServiceEndpoint(typeof (IVanillaService), new NetTcpBinding(),
                "net.tcp://localhost:9010/VanillaService");
            serviceHost.Open();

            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Blending");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            var bus = Bus.Create(configuration).Start();

            //var factory = new ChannelFactory<IVanillaService>();
            //var client = factory.CreateChannel(new EndpointAddress("net.tcp://localhost:9010/VanillaService"));

            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
