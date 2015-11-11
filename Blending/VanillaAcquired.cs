using NServiceBus;

namespace Blending
{
    public class VanillaAcquired : IMessage
    {
        public int LotNumber { get; set; }

        public Vanilla Vanilla { get; set; }
    }
}