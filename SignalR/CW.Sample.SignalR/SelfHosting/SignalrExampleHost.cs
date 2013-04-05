using System;
using System.Threading;
using CW.Sample.SignalR.SelfHosting.Hubs;
using Microsoft.Owin.Hosting;

namespace CW.Sample.SignalR.SelfHosting
{
    public class SignalrExampleHost
    {
        private readonly string _address;
        public Thread Thread { get; set; }

        public SignalrExampleHost(string address)
        {
            _address = address;
        }

        public bool Active { get; set; }

        public void Start(bool threadMode)
        {
            if (threadMode)
            {
                if (Thread == null)
                {
                    Thread = new Thread(StartServer) { IsBackground = true, };
                    Thread.Start();

                    while (!Active)
                    {
                        System.Threading.Thread.Sleep(10);
                    }    
                }
            }
            else
            {
                StartServer();
            }
        }

        private void StartServer()
        {
            try
            {
                using (WebApplication.Start<Startup>(_address))
                {
                    Active = true;
                    while (Active)
                    {
                        Thread.Sleep(10);
                    }
                }   
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SignalrExampleHost: " + ex);
            }
            
        }
    }
}