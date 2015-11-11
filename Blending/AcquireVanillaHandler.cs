using NServiceBus;

namespace Blending
{
    public class AcquireVanillaHandler : IHandleMessages<AcquireVanilla>
    {
        private readonly Communicator communicator;

        public AcquireVanillaHandler(Communicator communicator)
        {
            this.communicator = communicator;
        }

        public IBus Bus { get; set; }

        public void Handle(AcquireVanilla message)
        {
            var vanilla = communicator.AcquireVanilla().Result;

            Bus.Reply(new VanillaAcquired { LotNumber = message.LotNumber, Vanilla = vanilla });
        }
    }
}