using NServiceBus;

namespace Blending
{
    public class AcquireVanilla : ICommand
    {
        public int LotNumber { get; set; }
    }
}