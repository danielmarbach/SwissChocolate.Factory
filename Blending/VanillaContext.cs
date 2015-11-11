using System.Data.Entity;

namespace Blending
{
    public class VanillaContext : DbContext
    {
        public DbSet<VanillaUsage> Usages { get; set; } 
    }
}