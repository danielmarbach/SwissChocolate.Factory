using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Grinding
{
    public class BeansGrinder : IHandleMessages<GrindBeans>
    {
        public async Task Handle(GrindBeans message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Handler] Grinding beans");
            Console.WriteLine($"['{message.LotNumber}' - Handler] Liquifying beans");

            if (message.LotNumber % 2 == 0)
            {
                Console.WriteLine($"['{message.LotNumber}' - Handler] Producing cocoa solid");
                await context.SendLocal(new ProduceCocoaSolid { LotNumber = message.LotNumber });
            }
            else
            {
                Console.WriteLine($"['{message.LotNumber}' - Handler] Producing cocoa butter");
                await context.SendLocal(new ProduceCocoaButter { LotNumber = message.LotNumber });
            }

            await context.Publish(new BeansGround { LotNumber = message.LotNumber });
        }
    }
}