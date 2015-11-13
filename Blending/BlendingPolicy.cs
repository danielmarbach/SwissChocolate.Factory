using System;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace Blending
{
    public class BlendingPolicy : Saga<BlendingPolicyData>,
        IAmStartedByMessages<BlendChocolate>,
        IHandleMessages<VanillaAcquired>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BlendingPolicyData> mapper)
        {
            mapper.ConfigureMapping<BlendChocolate>(m => m.LotNumber).ToSaga(s => s.LotNumber);
        }

        public void Handle(BlendChocolate message)
        {
            Data.LotNumber = message.LotNumber;

            SpecialConsole.WriteLine($"['{message.LotNumber}' - Policy] Acquiring vanilla");

            Bus.SendLocal(new AcquireVanilla { LotNumber = message.LotNumber });
        }

        public void Handle(VanillaAcquired message)
        {
            SpecialConsole.WriteLine($"['{message.LotNumber}' - Policy] Chocolate blended");

            Bus.Publish(new ChocolateBlended { LotNumber = message.LotNumber });

            MarkAsComplete();
        }
    }
}