using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Blending
{
    public class AcquireVanillaHandler : IHandleMessages<AcquireVanilla>
    {
        private readonly Communicator communicator;
        private readonly VanillaContext vanillaContext;

        public AcquireVanillaHandler(Communicator communicator, VanillaContext vanillaContext)
        {
            this.vanillaContext = vanillaContext;
            this.communicator = communicator;
        }

        public async Task Handle(AcquireVanilla message, IMessageHandlerContext context)
        {
            SpecialConsole.WriteLine($"['{message.LotNumber}' - Handler] Acquire vanilla");

            var vanilla = await communicator.AcquireVanilla(message.LotNumber);

            using (var transaction = vanillaContext.Database.BeginTransaction())
            {
                vanillaContext.Usages.Add(new VanillaUsage(message.LotNumber));

                await context.ReplyAsync(new VanillaAcquired { LotNumber = message.LotNumber, Vanilla = vanilla });

                SpecialConsole.WriteLine($"['{message.LotNumber}' - Handler] Saving vanilla stats");

                await vanillaContext.SaveChangesAsync();

                transaction.Commit();
            }
        }
    }
}