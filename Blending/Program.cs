﻿using System;
using System.Data.Entity;
using System.IO;
using System.ServiceModel;
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
            // Force DB creation outside of TxScope
            File.Delete(@".\Vanilla.sdf");
            Database.SetInitializer(new CreateDatabaseIfNotExists<VanillaContext>());
            var context = new VanillaContext();
            context.Database.CreateIfNotExists();

            var serviceHost = new ServiceHost(typeof(VanillaService));
            serviceHost.AddServiceEndpoint(typeof(IVanillaService), new NetTcpBinding(),
                Constants.VanillaServiceAddress.Uri);
            serviceHost.Open();

            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Error);

            var container = new Container(x =>
            {
                x.ForConcreteType<VanillaContext>().Configure.SetLifecycleTo(Lifecycles.Container);
                x.ForConcreteType<Communicator>().Configure.SetLifecycleTo(Lifecycles.Container);
                x.For<ChannelFactory<IVanillaService>>().Use(() => new ChannelFactory<IVanillaService>(new NetTcpBinding())).SetLifecycleTo(Lifecycles.Container);
            });

            var configuration = new BusConfiguration();

            configuration.ExcludeAssemblies("System.Data.SqlServerCe.dll");

            configuration.EndpointName("Chocolate.Blending");

            configuration.Transactions().DoNotWrapHandlersExecutionInATransactionScope();
            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(container));

            var bus = Endpoint.StartAsync(configuration).GetAwaiter().GetResult();

            Console.ReadLine();

            bus.StopAsync().GetAwaiter().GetResult();
            serviceHost.Close();
        }
    }
}
