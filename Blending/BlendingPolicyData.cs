using NServiceBus.Saga;

namespace Blending
{
    public class BlendingPolicyData : ContainSagaData
    {
        [Unique]
        public int LotNumber { get; set; }
    }
}