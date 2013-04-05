using NUnit.Framework;

namespace CW.Sample.SignalR.SelfHosting
{
    [TestFixture]
    public class SignalrExampleUnitTests
    {
        [Test]
        public void StartServer()
        {
            var host = new SignalrExampleHost("http://localhost:8080/");
            host.Start();
            host.Thread.Join();
        }

    }
}