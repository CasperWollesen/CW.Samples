using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CW.Sample.SignalR.SelfHosting.Hubs
{
    // [HubName("SignalrExampleHub")]
    public class SignalrExampleHub : Hub
    {
        public SignalrExampleHub()
        {
            System.Diagnostics.Debug.WriteLine("SignalrExampleHub: SignalrExampleHub");
        }

        public void BroadcastSignalrMethod(BroadcastSignalrMethodModel model)
        {
            System.Diagnostics.Debug.WriteLine("SignalrExampleHub: BroadcastSignalrMethod");
            Clients.All.broadcastSignalrMethod(model);
        }

        public SimpleSignalrMethodResponseModel SimpleSignalrMethod(SimpleSignalrMethodRequestModel request)
        {
            System.Diagnostics.Debug.WriteLine("SignalrExampleHub: SimpleSignalrMethod");
            return new SimpleSignalrMethodResponseModel {Message = request.Message.ToLower() };
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
}