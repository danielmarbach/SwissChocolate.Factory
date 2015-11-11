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
        }

        public void Handle(BlendChocolate message)
        {
            Data.LotNumber = message.LotNumber;

            Bus.SendLocal(new AcquireVanilla { LotNumber = message.LotNumber });
        }

        public void Handle(VanillaAcquired message)
        {
            Bus.Publish(new ChocolateBlended());

            MarkAsComplete();
        }
    }
}