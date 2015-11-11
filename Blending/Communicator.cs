using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blending
{
    public class Communicator
    {
        [ThreadStatic]
        static readonly ChannelFactory<IVanillaService> factory = new ChannelFactory<IVanillaService>(new NetTcpBinding());

        public Task<Vanilla> AcquireVanilla()
        {
            var client = factory.CreateChannel(Constants.VanillaServiceAddress);
            return client.GetVanilla();
        } 
    }
}