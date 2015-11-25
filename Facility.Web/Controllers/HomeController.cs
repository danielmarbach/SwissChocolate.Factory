using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Messages;
using NServiceBus;

namespace Facility.Web.Controllers
{
    public class HomeController : Controller
    {
        static readonly Random Random = new Random();

        private readonly IBusContext bus;

        public HomeController(IBusContext bus)
        {
            this.bus = bus;
        }

        public async Task<ActionResult> Index()
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

                await bus.Send(new ProduceChocolateBar { LotNumber = lotNumber });

                context.Productions.Add(new ChocolateProduction(lotNumber));

                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}