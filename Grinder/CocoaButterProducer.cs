using System;
using NServiceBus;

namespace Grinding
{
    public class CocoaButterProducer : IHandleMessages<ProduceCocoaButter>
    {
        public void Handle(ProduceCocoaButter message)
        {
            Console.WriteLine($"['{message.LotNumber}' - Handler] Produced cocoa butter");
        }
    }
}