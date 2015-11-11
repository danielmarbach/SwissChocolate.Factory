using NServiceBus;

namespace Messages
{
    public class BeansRoasted : IEvent
    {
        public int LotNumber { get; set; }
    }
}