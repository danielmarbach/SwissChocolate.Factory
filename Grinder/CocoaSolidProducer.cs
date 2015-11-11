using System;
using NServiceBus;

namespace Grinding
{
    public class CocoaSolidProducer : IHandleMessages<ProduceCocoaSolid>
    {
        public void Handle(ProduceCocoaSolid message)
        {
            Console.WriteLine("Produced cocoa solid");
        }
    }
}