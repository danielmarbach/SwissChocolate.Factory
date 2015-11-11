using System;
using NServiceBus;

namespace Blending
{
    public class AcquireVanillaHandler : IHandleMessages<AcquireVanilla>
    {
        private readonly Communicator communicator;
        private readonly VanillaContext context;

        public AcquireVanillaHandler(Communicator communicator, VanillaContext context)
        {
            this.context = context;
            this.communicator = communicator;
        }

        public IBus Bus { get; set; }

        public void Handle(AcquireVanilla message)
        {
            var vanilla = communicator.AcquireVanilla().Result;

            using (var transaction = context.Database.BeginTransaction())
            {
                context.Usages.Add(new VanillaUsage(DateTimeOffset.UtcNow));

                Bus.Reply(new VanillaAcquired {LotNumber = message.LotNumber, Vanilla = vanilla});

                context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}