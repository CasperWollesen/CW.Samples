﻿using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Client;
using CW.Sample.WCF.WcfExampleService.Host;
using CW.Sample.WCF.WcfExampleService.Models;
using NUnit.Framework;

namespace CW.Sample.WCF.WcfExampleService.UnitTests
{
    [TestFixture]
    public class WcfExampleServiceUnitTests
    {
        [Test]
        public void StartHostAndClient_SimpleSyncMethodSyncCall_Success()
        {
            using (var host = new WcfExampleServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient("localhost:10000");
                SimpleSyncMethodResponseModel responseSimpleSyncMethod = client.SimpleSyncMethod(new SimpleSyncMethodRequestModel { Message = "Hello World" });
                Assert.IsNotNull(responseSimpleSyncMethod);
                Assert.AreEqual("SimpleSyncMethod: Hello World", responseSimpleSyncMethod.Message);

                host.Close();
            }
        }


       

        /// <summary>
        /// Very important, if host is started on same thread as client, and client is called on Sync method, then it will time out.
        /// See WcfExampleServiceUnitTestsProblem -> StartHostAndClient_SimpleSyncMethodSyncCallWithAsync_ThrowsException.
        /// Solution is to start Host on another thread.
        /// </summary>
        [Test]
        public async void StartHostAndClient_SimpleSyncMethodSyncCallWithAsync_Success()
        {
            bool @continue = false;
            var thread = new System.Threading.Thread(() =>
            {
                using (var host = new WcfExampleServiceHost("localhost:10000"))
                {
                    host.Start();
                    @continue = true;
                    while (@continue)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                    host.Close();
                }
            });
            thread.Start();

            while (!@continue)
            {
                System.Threading.Thread.Sleep(10);
            }

            var client = new WcfExampleServiceAsyncClient("localhost:10000");
            SimpleSyncMethodResponseModel responseSimpleSyncMethod = client.SimpleSyncMethod(new SimpleSyncMethodRequestModel { Message = "Hello World" });
            Assert.IsNotNull(responseSimpleSyncMethod);
            Assert.AreEqual("SimpleSyncMethod: Hello World", responseSimpleSyncMethod.Message);

            @continue = false;

            thread.Join();
        }

        [Test]
        public async void StartHostAndClient_SimpleSyncMethodAsyncCall_Success()
        {
            using (var host = new WcfExampleServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient("localhost:10000");
                SimpleSyncMethodResponseModel responseSimpleSyncMethod = await client.SimpleSyncMethodAsync(new SimpleSyncMethodRequestModel { Message = "Hello World" });
                Assert.IsNotNull(responseSimpleSyncMethod);
                Assert.AreEqual("SimpleSyncMethod: Hello World", responseSimpleSyncMethod.Message);

                host.Close();
            }
        }

        [Test]
        public async void StartHostAndClient_SimpleTaskMethodAsyncCall_Success()
        {
            using (var host = new WcfExampleServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient("localhost:10000");

                SimpleTaskMethodResponseModel responseSimpleTaskMethod = await client.SimpleTaskMethod(new SimpleTaskMethodRequestModel { Size = 100 });
                Assert.IsNotNull(responseSimpleTaskMethod);
                Assert.IsNotNull(responseSimpleTaskMethod.Result);
                Assert.AreEqual(100, responseSimpleTaskMethod.Result.Length);

                host.Close();
            }
        }

        [Test]
        public async void StartHostAndClient_SimpleTaskMethodSyncCall1_Success()
        {
            using (var host = new WcfExampleServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient("localhost:10000");

                Task<SimpleTaskMethodResponseModel> responseSimpleTaskMethod = client.SimpleTaskMethod(new SimpleTaskMethodRequestModel { Size = 100 });
                Assert.IsNotNull(responseSimpleTaskMethod);

                await responseSimpleTaskMethod;

                Assert.IsTrue(responseSimpleTaskMethod.IsCompleted);
                Assert.IsFalse(responseSimpleTaskMethod.IsCanceled);
                Assert.IsFalse(responseSimpleTaskMethod.IsFaulted);

                Assert.IsNotNull(responseSimpleTaskMethod.Result);
                Assert.IsNotNull(responseSimpleTaskMethod.Result.Result);
                Assert.AreEqual(100, responseSimpleTaskMethod.Result.Result.Length);

                host.Close();
            }
        }

        [Test]
        public async void StartHostAndClient_SimpleTaskMethodSyncCall2_Success()
        {
            using (var host = new WcfExampleServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new WcfExampleServiceAsyncClient("localhost:10000");

                Task<SimpleTaskMethodResponseModel> responseSimpleTaskMethod = client.SimpleTaskMethod(new SimpleTaskMethodRequestModel { Size = 100 });
                Assert.IsNotNull(responseSimpleTaskMethod);

                while (!responseSimpleTaskMethod.IsCompleted)
                {
                    // Do some work!
                    await Task.Factory.StartNew(() => System.Threading.Thread.Sleep(10));
                }

                Assert.IsTrue(responseSimpleTaskMethod.IsCompleted);
                Assert.IsFalse(responseSimpleTaskMethod.IsCanceled);
                Assert.IsFalse(responseSimpleTaskMethod.IsFaulted);

                Assert.IsNotNull(responseSimpleTaskMethod.Result);
                Assert.IsNotNull(responseSimpleTaskMethod.Result.Result);
                Assert.AreEqual(100, responseSimpleTaskMethod.Result.Result.Length);

                host.Close();
            }
        }
    }
}