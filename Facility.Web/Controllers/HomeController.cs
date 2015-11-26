using System;
using System.Web.Mvc;
using Messages;
using NServiceBus;

namespace Facility.Web.Controllers
{
    public class HomeController : Controller
    {
        static readonly Random Random = new Random();

        private readonly ISendOnlyBus bus;

        public HomeController(ISendOnlyBus bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("produce")]
        [HttpPost]
        public ActionResult Produce()
        {
            using (var context = new ChocolateContext())
            {
                var lotNumber = Random.Next(1, 9999);

                bus.Send(new ProduceChocolateBar { LotNumber = lotNumber });

                context.Productions.Add(new ChocolateProduction(lotNumber));

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}