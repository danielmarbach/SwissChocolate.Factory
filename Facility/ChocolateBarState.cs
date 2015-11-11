using NServiceBus.Saga;

namespace Facility
{
    public class ChocolateBarState : ContainSagaData
    {
        [Unique]
        public int LotNumber { get; set; }
    }
}