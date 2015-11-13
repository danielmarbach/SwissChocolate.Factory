using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Roasting
{
    public class BeansRoaster : IHandleMessages<RoastBeans>
    {
        public Task Handle(RoastBeans message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Handler] Roasting beans");
            Console.WriteLine($"['{message.LotNumber}' - Handler] Winnowing beans");

            return context.PublishAsync(new BeansRoasted { LotNumber = message.LotNumber });
        }
    }
}