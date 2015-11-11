using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Facility
{
    public class AuditConfigurationProvider : IProvideConfiguration<AuditConfig>
    {
        public AuditConfig GetConfiguration()
        {
            return new AuditConfig { QueueName = "audit" };
        }
    }
}