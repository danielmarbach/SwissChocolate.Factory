using System;
using Messages;
using NServiceBus;

namespace Roasting
{
    public class BeansRoaster : IHandleMessages<RoastBeans>
    {
        public IBus Bus { get; set; }

        public void Handle(RoastBeans message)
        {
            Console.WriteLine($"['{message.LotNumber}' - Handler] Roasting beans");
            Console.WriteLine($"['{message.LotNumber}' - Handler] Winnowing beans");

            Bus.Publish(new BeansRoasted { LotNumber = message.LotNumber });
        }
    }
}