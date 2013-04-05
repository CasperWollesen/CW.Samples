using System;
using System.Threading.Tasks;
using CW.Sample.SignalR.SelfHosting.Hubs;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace CW.Sample.SignalR.SelfHosting
{
    public class SignalrExampleClient
    {
        private readonly string _address;

        public HubConnection Connection { get; private set; }
        public IHubProxy Proxy { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">Ex. http://localhost:8080/</param>
        /// <param name="name"></param>
        public SignalrExampleClient(string address, string name)
        {
            _address = address;
            _name = name; 
        }


        private bool _ready;
        private string _name;

        private void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine( string.Format("{0}: {1}", _name, message) );
        }

        public async Task<int> Initialize()
        {
            if (!_ready)
            {
                if (Connection == null)
                {
                    Connection = new HubConnection(_address);

                    Connection.Closed += delegate { Debug("Closed"); };
                    Connection.Error += delegate(Exception exception) { Debug("Error: " + exception); };
                    Connection.Received += delegate(string s) { Debug("Received: " + s.Substring(10) ); };
                    Connection.Reconnected += () => Debug("Reconnected");
                    Connection.Reconnecting += () => Debug("Reconnecting");
                    Connection.StateChanged += delegate(StateChange change) { Debug( string.Format("StateChanged: Old: {0}, New: {1}", change.OldState, change.NewState)); };

                    Proxy = Connection.CreateHubProxy("SignalrExampleHub");

                    Proxy.On<BroadcastSignalrMethodModel>("broadcastSignalrMethod", OnBroadcastSignalrMethod);

                    await Connection.Start();

                    _ready = true;
                }
            }

            return 0;
        }

       
        public async Task<int> BroadcastSignalrMethod(BroadcastSignalrMethodModel model)
        {
            await Initialize();

            if (Proxy != null)
            {
                await Proxy.Invoke<BroadcastSignalrMethodModel>("broadcastSignalrMethod", model);
            }

            return 0;
        }

        public async Task<SimpleSignalrMethodResponseModel> SimpleSignalrMethod(SimpleSignalrMethodRequestModel request)
        {
            try
            {
                await Initialize();

                if (Proxy != null)
                {
                    SimpleSignalrMethodResponseModel responseModel = await Proxy.Invoke<SimpleSignalrMethodResponseModel>("simpleSignalrMethodx", request);

                    Debug(string.Format("SimpleSignalrMethod, Message: {0}", responseModel.Message));

                    /*
                             .ContinueWith(task =>
                             {
                                 responseModel = task.Result;
                                 Debug(string.Format("Message: {0}", responseModel.Message));
                             });
                    */
                    return responseModel;
                }

                

            }
            catch (Exception ex)
            {
                Debug(string.Format("SimpleSignalrMethod Exception: {0}", ex)); 
            }

            return null;
            
        }

        private void OnBroadcastSignalrMethod(BroadcastSignalrMethodModel model)
        {
            Debug(string.Format("OnBroadcastSignalrMethod, Message: {0}", model.Message));
        }

        
       

        
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

