using NServiceBus;
using NServiceBus.Sagas;

namespace Facility
{
    public class ChocolateBarState : ContainSagaData
    {
        public int LotNumber { get; set; }
    }
}