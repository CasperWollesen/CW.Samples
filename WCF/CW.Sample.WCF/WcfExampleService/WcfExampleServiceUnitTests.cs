using System.ServiceModel;
using CW.Sample.WCF.WcfExampleService.Models;
using NUnit.Framework;

namespace CW.Sample.WCF.WcfExampleService
{
    [TestFixture]
    public class WcfExampleServiceUnitTests
    {
        [Test]
        public void Test()
        {
            using (var host = new WcfExampleServiceHost())
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient(WcfExampleServiceConfig.CreateBinding(), new EndpointAddress(WcfExampleServiceConfig.CreateUrl("localhost:10000")));
                SimpleSyncMethodResponseModel response = client.SimpleSyncMethod(new SimpleSyncMethodRequestModel());


            }
        }
    }
}