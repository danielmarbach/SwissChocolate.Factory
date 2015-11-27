using NServiceBus;

namespace Facility
{
    public class ChocolateBarState : ContainSagaData
    {
        public int LotNumber { get; set; }
    }
}