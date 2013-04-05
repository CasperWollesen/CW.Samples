using System.Threading;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.Owin.Hosting;

namespace CW.Sample.SignalR.SelfHosting
{
    public class SignalrExampleHost
    {
        public void Start()
        {
            using (WebApplication.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                // Console.ReadLine();
                bool @continue = true;
                while (@continue && sleep)
                {
                    System.Threading.Thread.Sleep(500);
                }
            }   
        }
    }

    public class SignalrExampleClient
    {
        private readonly string _address;

        public bool Active { get; set; }

        public Thread Thread { get; private set; }
        public HubConnection Connection { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">Ex. http://localhost:8080/</param>
        public SignalrExampleClient(string address)
        {
            _address = address;
        }
        
        private void ThreadStart()
        {
            Active = true;
            Connection = new HubConnection(_address);
            Connection.Start();

            while (Active)
            {
                Thread.Sleep(10);
            }
        }

        public async void Start()
        {
            Thread = new Thread(ThreadStart);
            Thread.Start();
        }
    }

    /*
    
      Proxy = Connection.CreateHubProxy("Chat");
        public IHubProxy Proxy { get; private set; }

        var data = new RequestDataModel { CallerName = "G", Value = 12, Buffer = null, };
                var passValueStartTime = DateTime.Now;
                await chat.Invoke<RequestDataModel>("passValue", data).ContinueWith(task =>
                {
                    var model = task.Result;
                    Console.WriteLine(string.Format("passValue: Id: {0} Value: {1} BufferLength: {2}, Time: {3}", model.CallerName, model.Value, model.Buffer.Length, (DateTime.Now - passValueStartTime).TotalMilliseconds));
                });

        private async Task PassValueAsync()
        {
            
        }

        [Test]
        public void OnValueAction()
        {
            OnValueAsync().Wait();
        }

        private async Task OnValueAsync()
        {
            var connection = new HubConnection("http://localhost:8080/");

            connection.Closed += delegate { System.Diagnostics.Debug.WriteLine("Closed"); };
            connection.Error += delegate(Exception exception) { System.Diagnostics.Debug.WriteLine("Error"); };
            connection.Received += delegate(string s) { System.Diagnostics.Debug.WriteLine("Received"); };
            connection.Reconnected += delegate { System.Diagnostics.Debug.WriteLine("Reconnected"); };
            connection.Reconnecting += delegate { System.Diagnostics.Debug.WriteLine("Reconnecting"); };
            connection.StateChanged += delegate(StateChange change) { System.Diagnostics.Debug.WriteLine("StateChanged"); };

            IHubProxy chat = connection.CreateHubProxy("Chat");

            DateTime startTime = DateTime.MinValue;

            // chat.On<string>("send", Console.WriteLine);
            chat.On<RequestDataModel>("requestData",
                                      model =>
                                      {
                                          // var executionTime = DateTime.Now - startTime;
                                          System.Diagnostics.Debug.WriteLine(string.Format("[SERVER] OnRequestData: Id: {0} Value: {1} BufferLength: {2}, NumberOfClientNames: {4} ({3})", 
                                              model.CallerName, model.Value, model.Buffer.Length, DateTime.Now,  model.ClientsNames != null ? model.ClientsNames.Length : -1 ));
                                      }
                                      );

            await connection.Start();

            var firstTime = true;

            while (true)
            {
                // await chat.Invoke("send", "Console: " + line);
                // await chat.Invoke<RequestDataModel>("requestData", data);
                if (firstTime)
                {
                    await chat.Invoke<RequestDataModel>("requestData", new RequestDataModel { CallerName = "Client: " + DateTime.Now, Value = 2, Buffer = new byte[2]});
                    firstTime = false;
                }


                System.Threading.Thread.Sleep(500);
            }
        }
    */
}
