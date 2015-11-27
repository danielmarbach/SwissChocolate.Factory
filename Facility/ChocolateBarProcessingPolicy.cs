using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Facility
{
    public class ChocolateBarProcessingPolicy : Saga<ChocolateBarState>,
        IAmStartedByMessages<ProduceChocolateBar>,
        IHandleMessages<BeansRoasted>,
        IHandleMessages<BeansGround>,
        IHandleMessages<ChocolateBlended>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ChocolateBarState> mapper)
        {
            mapper.ConfigureMapping<ProduceChocolateBar>(m => m.LotNumber).ToSaga(s => s.LotNumber);
            mapper.ConfigureMapping<BeansRoasted>(m => m.LotNumber).ToSaga(s => s.LotNumber);
            mapper.ConfigureMapping<BeansGround>(m => m.LotNumber).ToSaga(s => s.LotNumber);
        }

        public Task Handle(ProduceChocolateBar message, IMessageHandlerContext context)
        {
            Data.LotNumber = message.LotNumber;

            Console.WriteLine($"['{message.LotNumber}' - Policy] Start roasting");
            return context.Send(new RoastBeans { LotNumber = Data.LotNumber });
        }

        public Task Handle(BeansRoasted message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Policy] Start grinding");
            return context.Send(new GrindBeans { LotNumber = Data.LotNumber });
        }

        public Task Handle(BeansGround message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Policy] Start blending");
            return context.Send(new BlendChocolate { LotNumber = Data.LotNumber });
        }

        public Task Handle(ChocolateBlended message, IMessageHandlerContext context)
        {
            Console.WriteLine($"['{message.LotNumber}' - Policy] Done");
            MarkAsComplete();

            return Task.FromResult(0);
        }
    }
}
