using System;
using System.ServiceModel;
using CW.Sample.WCF.WcfExampleService.Interface;

namespace CW.Sample.WCF.WcfExampleService
{
    public class WcfExampleServiceHost : IDisposable
    {
        private ServiceHost _host;

        public void Start()
        {
            _host = new ServiceHost(typeof (CW.Sample.WCF.WcfExampleService.WcfExampleService));
            _host.AddServiceEndpoint(typeof(IWcfExampleService), WcfExampleServiceConfig.CreateBinding(), WcfExampleServiceConfig.CreateUrl("localhost:10000"));
            _host.Open();
        }

        private void Close()
        {
            if (_host != null)
            {
                _host.Close();
            }
        }

        public void Dispose()
        {
            if (_host != null)
            {
                Close();
                var disposer = _host as IDisposable;
                disposer.Dispose();
            }
        }
    }
}