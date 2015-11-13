using NServiceBus;

namespace Blending
{
    public class BlendingPolicyData : ContainSagaData
    {
        public int LotNumber { get; set; }
    }
}