using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Interface;
using CW.Sample.WCF.WcfExampleService.Models;

namespace CW.Sample.WCF.WcfExampleService.Client
{
    public class WcfExampleServiceAsyncClient : WcfExampleServiceClientBase<IWcfExampleServiceAsync>
    {
        public WcfExampleServiceAsyncClient(string address) :
            base(address)
        {
        }

        public WcfExampleServiceAsyncClient(string address, int port) :
            base(address, port)
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