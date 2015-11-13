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
            SpecialConsole.WriteLine($"['{message.LotNumber}' - Handler] Acquire vanilla");

            var vanilla = communicator.AcquireVanilla(message.LotNumber).Result;

            using (var transaction = context.Database.BeginTransaction())
            {
                context.Usages.Add(new VanillaUsage(message.LotNumber));

                Bus.Reply(new VanillaAcquired { LotNumber = message.LotNumber, Vanilla = vanilla });

                SpecialConsole.WriteLine($"['{message.LotNumber}' - Handler] Saving vanilla stats");

                context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}