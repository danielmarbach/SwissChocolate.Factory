using System;
using Messages;
using NServiceBus;

namespace Grinding
{
    public class BeansGrinder : IHandleMessages<GrindBeans>
    {
        public IBus Bus { get; set; }

        public void Handle(GrindBeans message)
        {
            Console.WriteLine($"['{message.LotNumber}'] Grinding beans");
            Console.WriteLine($"['{message.LotNumber}'] Liquifying beans");

            if (message.LotNumber%2 == 0)
            {
                Console.WriteLine($"['{message.LotNumber}'] Producing cocoa solid");
                Bus.SendLocal(new ProduceCocoaSolid { LotNumber = message.LotNumber });
            }
            else
            {
                Console.WriteLine($"['{message.LotNumber}'] Producing cocoa butter");
                Bus.SendLocal(new ProduceCocoaButter { LotNumber = message.LotNumber });
            }
            
            Bus.Publish(new BeansGround { LotNumber = message.LotNumber });
        }
    }
}