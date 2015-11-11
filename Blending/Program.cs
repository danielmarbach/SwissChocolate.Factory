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
                Constants.VanillaServiceAddress.Uri);
            serviceHost.Open();

            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Blending");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            var bus = Bus.Create(configuration).Start();

            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
