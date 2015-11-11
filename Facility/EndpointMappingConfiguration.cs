using Messages;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Facility
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
                    TypeFullName = typeof (RoastBeans).FullName,
                    Endpoint = "Chocolate.Roasting"
                },
                new MessageEndpointMapping
                {
                    AssemblyName = "Messages",
                    TypeFullName = typeof (BeansRoasted).FullName,
                    Endpoint = "Chocolate.Roasting"
                },
                new MessageEndpointMapping
                {
                    AssemblyName = "Messages",
                    TypeFullName = typeof (GrindBeans).FullName,
                    Endpoint = "Chocolate.Grinding"
                },
                new MessageEndpointMapping
                {
                    AssemblyName = "Messages",
                    TypeFullName = typeof (BeansGround).FullName,
                    Endpoint = "Chocolate.Grinding"
                },
                new MessageEndpointMapping
                {
                    AssemblyName = "Messages",
                    TypeFullName = typeof (BlendChocolate).FullName,
                    Endpoint = "Chocolate.Blending"
                },
                new MessageEndpointMapping
                {
                    AssemblyName = "Messages",
                    TypeFullName = typeof (ChocolateBlended).FullName,
                    Endpoint = "Chocolate.Blending"
                }
            };

            return new UnicastBusConfig { MessageEndpointMappings = mappings};
        }
    }
}