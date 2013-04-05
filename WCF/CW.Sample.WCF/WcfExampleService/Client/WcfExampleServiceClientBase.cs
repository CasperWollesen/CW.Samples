using System.ServiceModel;
using System.ServiceModel.Channels;
using CW.Sample.WCF.WcfExampleService.Config;

namespace CW.Sample.WCF.WcfExampleService.Client
{
    public class WcfExampleServiceClientBase<T> : ClientBase<T> where T : class 
    {
        public WcfExampleServiceClientBase(string address)
            : base(WcfExampleServiceConfig.CreateBinding(), WcfExampleServiceConfig.CreateEndpointAddress(address))
        {
        }

        public WcfExampleServiceClientBase(string address, int port)
            : base(WcfExampleServiceConfig.CreateBinding(), WcfExampleServiceConfig.CreateEndpointAddress(address, port))
        {
        }
    }
}