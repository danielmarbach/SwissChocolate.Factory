using NServiceBus;

namespace Messages
{
    public class BlendChocolate : ICommand
    {
        public int LotNumber { get; set; }
    }
}