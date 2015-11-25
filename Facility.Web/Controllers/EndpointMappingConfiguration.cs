using Messages;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Facility.Web.Controllers
{
    public class EndpointMappingConfiguration : IProvideConfiguration<UnicastBusConfig>
    {
        public UnicastBusConfig GetConfiguration()
        {
            var mappings = new MessageEndpointMappingCollection
            {
                new MessageEndpointMapping
                {
                    AssemblyName = "Messages",
                    TypeFullName = typeof (ProduceChocolateBar).FullName,
                    Endpoint = "Chocolate.Facility"
                },
            };

            return new UnicastBusConfig { MessageEndpointMappings = mappings };
        }
    }
}