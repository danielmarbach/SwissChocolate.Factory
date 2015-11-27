using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Grinding
{
    public class CocoaSolidProducer : IHandleMessages<ProduceCocoaSolid>
    {
        public Task Handle(ProduceCocoaSolid message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Handler] Produced cocoa solid");
            return Task.FromResult(0);
        }
    }
}