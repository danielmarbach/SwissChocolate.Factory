using NServiceBus;

namespace Messages
{
    public class BeansGround : IEvent
    {
        public int LotNumber { get; set; }
    }
}