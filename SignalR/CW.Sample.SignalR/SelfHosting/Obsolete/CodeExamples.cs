

// Kig på deres sample på github

//Install-Package Microsoft.Owin.Hosting -pre
//Install-Package Microsoft.Owin.Host.HttpListener -pre
//Install-Package Microsoft.AspNet.SignalR.Owin

namespace ST.Samples.SignalR.HostingSelf
{


    /*
    [TestFixture]
    public class SignalRDevAction
    {
        private bool _serverActive;

        [Test]
        public void StartServer()
        {
            string url = "http://localhost:8080";

            using (WebApplication.Start<Startup>(url))
            {
                _serverActive = true;
                while (_serverActive)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Turn cross domain on 
            var config = new HubConfiguration { EnableCrossDomain = true };

            // This will map out to http://localhost:8080/signalr by default
            app.MapHubs(config);
        }
    }

    public class MyHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.addMessage(message);
        }
    }
    */
    /*
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://127.0.0.1:8088/";
            var server = new Server(url);

            // Map the default hub url (/signalr)
            server.MapHubs();

            // Start the server
            server.Start();

            Console.WriteLine("Server running on {0}", url);

            // Keep going until somebody hits 'x'
            while (true)
            {
                ConsoleKeyInfo ki = Console.ReadKey(true);
                if (ki.Key == ConsoleKey.X)
                {
                    break;
                }
            }
        }

        [HubName("CustomHub")]
        public class MyHub : Hub
        {
            public string Send(string message)
            {
                return message;
            }

            public void DoSomething(string param)
            {
                Clients.addMessage(param);
            }
        }
    }
    */

    /*
    public class SignalRclientDevAction
    {
        private static void Main(string[] args)
        {
            //Set connection
            var connection = new HubConnection("http://127.0.0.1:8088/");
            //Make proxy to hub based on hub name on server

            var myHub = connection.CreateHubProxy("CustomHub");

            // var myHub = connection.CreateProxy("CustomHub");
            //Start connection

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    System.Diagnostics.Debug.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Connected");
                }

            }).Wait();

            myHub.Invoke<string>("Send", "HELLO World ").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    System.Diagnostics.Debug.WriteLine("There was an error calling send: {0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(task.Result);
                }
            });

            myHub.On<string>("addMessage", param =>
            {
                System.Diagnostics.Debug.WriteLine(param);
            });

            myHub.Invoke<string>("DoSomething", "I'm doing something!!!").Wait();

            var @continue = true;
            while (@continue)
            {
                System.Threading.Thread.Sleep(1000);
            }
            // Console.Read();
            connection.Stop();
        }
    }
    */
}
