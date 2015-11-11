using NServiceBus;

namespace Messages
{
    public class ProduceChocolateBar : ICommand
    {
        public int LotNumber { get; set; }
    }
}