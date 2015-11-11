using System.ServiceModel;

namespace Blending
{
    public static class Constants
    {
        public static EndpointAddress VanillaServiceAddress =
            new EndpointAddress("net.tcp://localhost:9010/VanillaService");
    }
}