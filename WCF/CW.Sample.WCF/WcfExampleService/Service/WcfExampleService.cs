using System;
using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Interface;
using CW.Sample.WCF.WcfExampleService.Models;

namespace CW.Sample.WCF.WcfExampleService.Service
{
    public class WcfExampleService : IWcfExampleService
    {
        public SimpleSyncMethodResponseModel SimpleSyncMethod(SimpleSyncMethodRequestModel request)
        {
            // var msg = string.Format("Hello, {0}!!", request.Message);
            // Console.WriteLine(msg);
            // System.Threading.Thread.Sleep(1000);
            return new SimpleSyncMethodResponseModel { Message = string.Format("SimpleSyncMethod: {0}", request.Message) };
        }

        public async Task<SimpleTaskMethodResponseModel> SimpleTaskMethod(SimpleTaskMethodRequestModel request)
        {
            // await Task.Factory.StartNew(() => System.Threading.Thread.Sleep(1000));
            return new SimpleTaskMethodResponseModel { Result = new byte[request.Size] };
        }
    }
}