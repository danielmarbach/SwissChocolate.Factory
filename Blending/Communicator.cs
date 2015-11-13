using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Blending
{
    public class Communicator
    {
        private readonly ChannelFactory<IVanillaService> factory;

        public Communicator(ChannelFactory<IVanillaService> factory)
        {
            this.factory = factory;
        }

        public Task<Vanilla> AcquireVanilla(int lotNumber)
        {
            var client = factory.CreateChannel(Constants.VanillaServiceAddress);
            return client.GetVanilla(lotNumber);
        } 
    }
}