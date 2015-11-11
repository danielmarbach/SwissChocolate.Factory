using NServiceBus;

namespace Grinding
{
    public class ProduceCocoaSolid : ICommand
    {
        public int LotNumber { get; set; }
    }
}