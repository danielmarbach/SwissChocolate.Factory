using NServiceBus;

namespace Grinding
{
    public class ProduceCocoaButter : ICommand
    {
        public int LotNumber { get; set; }
    }
}