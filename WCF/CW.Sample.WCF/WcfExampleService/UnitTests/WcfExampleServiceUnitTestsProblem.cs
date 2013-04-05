using System;
using System.ServiceModel;
using CW.Sample.WCF.WcfExampleService.Client;
using CW.Sample.WCF.WcfExampleService.Host;
using CW.Sample.WCF.WcfExampleService.Models;
using NUnit.Framework;

namespace CW.Sample.WCF.WcfExampleService.UnitTests
{
    [TestFixture]
    public class WcfExampleServiceUnitTestsProblem
    {
        /// <summary>
        /// Important, this test case only throws an exception, when StartHostAndClient_SimpleSyncMethodSyncCallWithAsync_ThrowsException has async 
        /// and calss SimpleSyncMethod called sync.
        /// </summary>
        [Test]
        public async void StartHostAndClient_SimpleSyncMethodSyncCallWithAsync_ThrowsException()
        {
            // If I uses using - then unit test never ends.
            // using (var host = new WcfExampleServiceHost("localhost:10000"))
            // {
            var host = new WcfExampleServiceHost("localhost:10000");

            host.Start();

            var client = new WcfExampleServiceAsyncClient("localhost:10000");
            SimpleSyncMethodResponseModel responseSimpleSyncMethod = null;
            Assert.Throws<CommunicationException>(() =>
                {
                    try
                    {
                        responseSimpleSyncMethod = client.SimpleSyncMethod(new SimpleSyncMethodRequestModel { Message = "Hello World" });
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                });

            Assert.IsNull(responseSimpleSyncMethod);

            // Close client.
            client.Abort();
            client.Close();
            var d = client as IDisposable;
            d.Dispose();


            // If I close and disponse - then unit test never ends.
            // host.Close();
            // host.Dispose();
            // }
        }
    }
}