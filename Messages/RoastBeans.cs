using NServiceBus;

namespace Messages
{
    public class RoastBeans : ICommand
    {
        public int LotNumber { get; set; }
    }
}