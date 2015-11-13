using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Blending
{
    public class Communicator
    {
        static readonly ThreadLocal<ChannelFactory<IVanillaService>> factory = new ThreadLocal<ChannelFactory<IVanillaService>>(() => new ChannelFactory<IVanillaService>(new NetTcpBinding()));

        public Task<Vanilla> AcquireVanilla(int lotNumber)
        {
            var channelFactory = factory.Value;
            var client = channelFactory.CreateChannel(Constants.VanillaServiceAddress);
            return client.GetVanilla(lotNumber);
        } 
    }
}