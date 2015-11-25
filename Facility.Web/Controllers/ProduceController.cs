using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Messages;
using NServiceBus;

namespace Facility.Web.Controllers
{
    public class ProduceController : Controller
    {
        static readonly Random Random = new Random();

        private readonly ISendOnlyBus bus;

        public ProduceController(ISendOnlyBus bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            using (var context = new ChocolateContext())
            {
                var lotNumber = Random.Next(1, 9999);

                bus.Send(new ProduceChocolateBar { LotNumber = lotNumber });

                context.Productions.Add(new ChocolateProduction(lotNumber));

                context.SaveChanges();
            }
            
            return View();
        }
    }
}