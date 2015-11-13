using System.Data.Entity;
using System.Reflection;

namespace Blending
{
    public class VanillaContext : DbContext
    {
        public VanillaContext() : base("Blending.VanillaContext")
        {
        }

        public DbSet<VanillaUsage> Usages { get; set; } 
    }
}