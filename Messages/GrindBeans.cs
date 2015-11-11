using NServiceBus;

namespace Messages
{
    public class GrindBeans : ICommand
    {
        public int LotNumber { get; set; }
    }
}