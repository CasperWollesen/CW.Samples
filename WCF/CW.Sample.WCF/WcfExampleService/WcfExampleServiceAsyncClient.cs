using System.ServiceModel;
using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Interface;
using CW.Sample.WCF.WcfExampleService.Models;

namespace CW.Sample.WCF.WcfExampleService
{
    public class WcfExampleServiceAsyncClient : ClientBase<IWcfExampleServiceAsync>, IWcfExampleServiceAsync
    {
        public WcfExampleServiceAsyncClient()
        {
        }

        public WcfExampleServiceAsyncClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public WcfExampleServiceAsyncClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public WcfExampleServiceAsyncClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public WcfExampleServiceAsyncClient(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public SimpleSyncMethodResponseModel SimpleSyncMethod(SimpleSyncMethodRequestModel request)
        {
            return Channel.SimpleSyncMethod(request);
        }

        public Task<SimpleTaskMethodResponseModel> SimpleTaskMethod(SimpleTaskMethodRequestModel request)
        {
            return Channel.SimpleTaskMethod(request);
        }

        public Task<SimpleSyncMethodResponseModel> SimpleSyncMethodAsync(SimpleSyncMethodRequestModel request)
        {
            return Channel.SimpleSyncMethodAsync(request);
        }
    }
}