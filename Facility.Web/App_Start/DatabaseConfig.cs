using System.Data.Entity;
using System.IO;
using Facility.Web.Controllers;

namespace Facility.Web
{
    public static class DatabaseConfig
    {
        public static void CreateDatabase()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ChocolateContext>());
            var context = new ChocolateContext();
            context.Database.CreateIfNotExists();
        }
    }
}