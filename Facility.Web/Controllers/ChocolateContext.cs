using System.Data.Entity;

namespace Facility.Web.Controllers
{
    public class ChocolateContext : DbContext
    {
        public ChocolateContext() : base("Facility.Web.ChocolateContext")
        {
        }

        public DbSet<ChocolateProduction> Productions { get; set; }
    }
}