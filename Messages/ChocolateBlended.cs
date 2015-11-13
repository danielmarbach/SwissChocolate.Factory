using NServiceBus;

namespace Messages
{
    public class ChocolateBlended : IEvent
    {
        public int LotNumber { get; set; }
    }
}