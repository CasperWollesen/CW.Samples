using System;
using System.Threading;
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

        public Thread Thread { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">Ex. http://localhost:8080/</param>
        /// <param name="name"></param>
        /// <param name="showDebug"></param>
        public SignalrExampleClient(string address, string name, bool showDebug = false)
        {
            _address = address;
            _name = name;
            _showDebug = showDebug;
        }


        private bool _ready;
        private string _name;
        private bool _showDebug;

        private void Debug(string message, bool forceUpdate = false)
        {
            if (_showDebug || forceUpdate)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", _name, message));
            }
            
        }

        public async Task<int> Initialize(bool subscribeBroadcastSignalrMethod = true)
        {
            if (!_ready)
            {
                /*Thread = new Thread( () => */
                                         {
                                             if (Connection == null)
                                             {
                                                 
                                                 Connection = new HubConnection(_address);

                                                 Connection.Closed += delegate { Debug("Closed"); };
                                                 Connection.Error += delegate(Exception exception) { Debug("Error: " + exception); };
                                                 Connection.Received += delegate(string s) { Debug("Received: " + s.Replace(Environment.NewLine, "")); };  // Substring(0, 10).
                                                 Connection.Reconnected += () => Debug("Reconnected");
                                                 Connection.Reconnecting += () => Debug("Reconnecting");
                                                 Connection.StateChanged += delegate(StateChange change) { Debug(string.Format("StateChanged: Old: {0}, New: {1}", change.OldState, change.NewState)); };

                                                 Proxy = Connection.CreateHubProxy("SignalrExampleHub");

                                                 if (subscribeBroadcastSignalrMethod)
                                                 {
                                                     Proxy.On<BroadcastSignalrMethodModel>("broadcastSignalrMethod", OnBroadcastSignalrMethod);    
                                                 }
                                                 
                                                 /* await */ Connection.Start();

                                                 Active = true;
                                                 _ready = true;
                                                 
                                                 // while (Active)
                                                 {
                                                    System.Threading.Thread.Sleep(10);    
                                                 }
                                             }
                                         } // );
                // Thread.Start();
                while (!_ready)
                {
                    await Task.Factory.StartNew(() => { Thread.Sleep(10); });
                } 
            }

            return 0;
        }

        public bool Active { get; set; }

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
                    SimpleSignalrMethodResponseModel responseModel = await Proxy.Invoke<SimpleSignalrMethodResponseModel>("simpleSignalrMethod", request);

                    Debug(string.Format("SimpleSignalrMethod, Message: {0}", responseModel.Message));
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
            
            Debug(string.Format("OnBroadcastSignalrMethod, Message: {0}", model.Message), true);
        }
    }
}  

