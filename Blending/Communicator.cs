using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blending
{
    public class Communicator
    {
        [ThreadStatic]
        static readonly ChannelFactory<IVanillaService> factory = new ChannelFactory<IVanillaService>();

        public Task<Vanilla> AcquireVanilla()
        {
            var client = factory.CreateChannel(new EndpointAddress("net.tcp://localhost:9010/VanillaService"));
            return client.GetVanilla();
        } 
    }
}