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

        private readonly IMessageSession session;

        public HomeController(IMessageSession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("produce")]
        [HttpPost]
        public async Task<ActionResult> Produce()
        {
            using (var context = new ChocolateContext())
            {
                var lotNumber = Random.Next(1, 9999);

                await session.Send(new ProduceChocolateBar { LotNumber = lotNumber });

                context.Productions.Add(new ChocolateProduction(lotNumber));

                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}