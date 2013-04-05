using System;
using CW.Sample.SignalR.SelfHosting.Hubs;
using NUnit.Framework;

namespace CW.Sample.SignalR.SelfHosting.UnitTests
{
    [TestFixture]
    public class SignalrExampleUnitTests
    {
        [Test]
        public async void StartServerClient()
        {
            var url = "http://localhost:8083/";
            var host = new SignalrExampleHost(url);
            host.Start(true);

            
            var client1 = new SignalrExampleClient(url, "Client 1");
            await client1.Initialize();

            SimpleSignalrMethodResponseModel r1 = await client1.SimpleSignalrMethod(new SimpleSignalrMethodRequestModel
                                                {
                                                    Message = "Hello world #1, " + DateTime.Now
                                                });

            SimpleSignalrMethodResponseModel r2 = await client1.SimpleSignalrMethod(new SimpleSignalrMethodRequestModel
            {
                Message = "Hello world #2, " + DateTime.Now
            });

            var b1 = await client1.BroadcastSignalrMethod(new BroadcastSignalrMethodModel { Message = "Hello world #1, " + DateTime.Now });
            var b2 = await client1.BroadcastSignalrMethod(new BroadcastSignalrMethodModel { Message = "Hello world #2, " + DateTime.Now });
          
            System.Threading.Thread.Sleep(15000);


            client1.Connection.Disconnect();
            host.Active = false;
            host.Thread.Join();
        }

        [Test]
        public async void StartServerClient2()
        {
            var url = "http://localhost:8083/";
            var host = new SignalrExampleHost(url);
            host.Start(true);


            var client1 = new SignalrExampleClient(url, "Client 1");
            await client1.Initialize();

            var client2 = new SignalrExampleClient(url, "Client 2");
            await client2.Initialize();

            SimpleSignalrMethodResponseModel r1 = await client1.SimpleSignalrMethod(new SimpleSignalrMethodRequestModel
            {
                Message = "Hello world #1, " + DateTime.Now
            });

            SimpleSignalrMethodResponseModel r2 = await client2.SimpleSignalrMethod(new SimpleSignalrMethodRequestModel
            {
                Message = "Hello world #2, " + DateTime.Now
            });

            var b1 = await client1.BroadcastSignalrMethod(new BroadcastSignalrMethodModel { Message = "Hello world #1, " + DateTime.Now });
            var b2 = await client2.BroadcastSignalrMethod(new BroadcastSignalrMethodModel { Message = "Hello world #2, " + DateTime.Now });

            System.Threading.Thread.Sleep(15000);


            client1.Connection.Disconnect(); 
            client2.Connection.Disconnect();
            host.Active = false;
            host.Thread.Join();
        }

    }
}