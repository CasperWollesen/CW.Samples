using System;
using System.Threading.Tasks;
using Shared.WcfExampleService.Interface;
using Shared.WcfExampleService.Models;

namespace Shared.WcfExampleService
{
    public class WcfExampleService : IWcfExampleService
    {
        public SimpleSyncMethodResponseModel SimpleSyncMethod(SimpleSyncMethodRequestModel request)
        {
            var msg = string.Format("Hello, {0}!!", request.Message);
            Console.WriteLine(msg);
            System.Threading.Thread.Sleep(1000);
            return new SimpleSyncMethodResponseModel { Message = msg };
        }

        public async Task<SimpleTaskMethodResponseModel> SimpleTaskMethod(SimpleTaskMethodRequestModel request)
        {
            await Task.Factory.StartNew(() => System.Threading.Thread.Sleep(1000));
            return new SimpleTaskMethodResponseModel { Result = new byte[request.Size] };
        }
    }
}