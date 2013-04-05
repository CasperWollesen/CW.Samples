using System.ServiceModel;
using CW.Sample.WCF.WcfExampleService.Client;
using CW.Sample.WCF.WcfExampleService.Config;
using CW.Sample.WCF.WcfExampleService.Host;
using CW.Sample.WCF.WcfExampleService.Models;
using NUnit.Framework;

namespace CW.Sample.WCF.WcfExampleService.UnitTests
{
    [TestFixture]
    public class WcfExampleServiceUnitTests
    {
        [Test]
        public void StartHostAndClient_Success()
        {
            using (var host = new WcfExampleServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient("localhost:10000");
                SimpleSyncMethodResponseModel response = client.SimpleSyncMethod(new SimpleSyncMethodRequestModel());
                Assert.IsNotNull(response);
            }
        }
    }
}