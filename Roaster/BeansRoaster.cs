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
            Console.WriteLine($"Roasting beans for lot '{message.LotNumber}'");
            Console.WriteLine($"Winnowing beans for lot '{message.LotNumber}'");

            Bus.Publish(new BeansRoasted { LotNumber = message.LotNumber });
        }
    }
}