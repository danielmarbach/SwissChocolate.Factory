using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Grinding
{
    public class CocoaButterProducer : IHandleMessages<ProduceCocoaButter>
    {
        public Task Handle(ProduceCocoaButter message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Handler] Produced cocoa butter");
            return Task.FromResult(0);
        }
    }
}