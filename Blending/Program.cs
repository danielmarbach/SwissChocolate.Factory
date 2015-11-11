using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using NServiceBus;
using NServiceBus.Logging;
using StructureMap;
using StructureMap.Pipeline;

namespace Blending
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<VanillaContext>());

            var serviceHost = new ServiceHost(typeof (VanillaService));
            serviceHost.AddServiceEndpoint(typeof (IVanillaService), new NetTcpBinding(),
                Constants.VanillaServiceAddress.Uri);
            serviceHost.Open();

            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Info);

            var container = new Container(x =>
            {
                x.ForConcreteType<VanillaContext>().Configure.SetLifecycleTo(Lifecycles.ThreadLocal);
                x.ForConcreteType<Communicator>().Configure.SetLifecycleTo(Lifecycles.Transient);
            });

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Blending");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(container));

            var bus = Bus.Create(configuration).Start();

            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
