using Microsoft.AspNet.SignalR;

namespace CW.Sample.SignalR.SelfHosting.Hubs
{
    public class SignalrExampleHub : Hub
    {
        public SignalrExampleHub()
        {
        }

        public void BroadcastSignalrMethod(BroadcastSignalrMethodModel model)
        {
            Clients.All.BroadcastSignalrMethod(model);
        }

        public SimpleSignalrMethodResponseModel SimpleSignalrMethod(SimpleSignalrMethodRequestModel request)
        {
            return new SimpleSignalrMethodResponseModel {Message = request.Message};
        }
    }

    public class BroadcastSignalrMethodModel
    {
        public string Message { get; set; }
    }

    public class SimpleSignalrMethodRequestModel
    {
        public string Message { get; set; }
    }

    public class SimpleSignalrMethodResponseModel
    {
        public string Message { get; set; }
    }

    /*
    
        public void Send(string message)
        {
            Clients.All.send(message);
        }

        private bool _firstTime = true;

        private readonly List<string> _clientNames = new List<string>();

        public void RequestData(RequestDataModel model)
        {
            if (!_clientNames.Contains(model.CallerName))
            {
                System.Diagnostics.Debug.WriteLine("[SERVER] RequestData, register caller: " + model.CallerName);
                _clientNames.Add(model.CallerName);
            }

            Clients.All.requestData(model);

            if (_firstTime)
            {
                StartService();
                _firstTime = false;
            }
        }

        public RequestDataModel2 PassValue(RequestDataModel2 model)
        {
            model.Buffer = new byte[1000000];
            return model;
        }
    
        private bool _requestDataActive;

        public async Task<int> StartService()
        {
            System.Diagnostics.Debug.WriteLine("[SERVER] Chat -> StartService Start (" + DateTime.Now + ")");
            Task.Factory.StartNew(delegate
                                      {
                                          _requestDataActive = true;
                                          var value = 0;
                                          while (_requestDataActive)
                                          {
                                              var startTime = DateTime.Now;
                                              RequestData(new RequestDataModel { CallerName = string.Format("SERVER"), Value = value++, Buffer = new byte[value], ClientsNames = _clientNames.ToArray() });
                                              System.Diagnostics.Debug.WriteLine("[SERVER] RequestData... in {0}MS ({1})", (DateTime.Now - startTime).TotalMilliseconds, DateTime.Now);
                                              System.Threading.Thread.Sleep(500);
                                          }
                                      });

            System.Diagnostics.Debug.WriteLine("[SERVER] Chat -> StartService End (" + DateTime.Now + ")");

            return 1;
        }

    public class RequestDataModel2
    {
        public byte[] Buffer { get; set; }
    }

    public class RequestDataModel
    {
        public string CallerName { get; set; }
        public string[] ClientsNames { get; set; }
        public int Value { get;set; }
        public byte[] Buffer { get; set; }
    }
    */
}