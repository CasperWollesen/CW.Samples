using System;
using System.ServiceModel;
using CW.Sample.WCF.WcfExampleService.Config;
using CW.Sample.WCF.WcfExampleService.Interface;

namespace CW.Sample.WCF.WcfExampleService.Host
{
    public class WcfExampleServiceHostServicesModel
    {
        public Type Type { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
    }

    public class WcfExampleServiceHostBase : IDisposable
    {
        protected ServiceHost _host;

        private readonly Type _serviceFacadeType;
        private readonly WcfExampleServiceHostServicesModel[] _serviceRelations;

        public WcfExampleServiceHostBase(Type serviceFacadeType, WcfExampleServiceHostServicesModel[] serviceRelations)
        {
            _serviceFacadeType = serviceFacadeType;
            _serviceRelations = serviceRelations;
        }

        public void Start()
        {
            _host = new ServiceHost(_serviceFacadeType);

            if (_serviceRelations != null)
            {
                foreach (WcfExampleServiceHostServicesModel relation in _serviceRelations)
                {
                    _host.AddServiceEndpoint(relation.Type, WcfExampleServiceConfig.CreateBinding(), WcfExampleServiceConfig.CreateUrl(relation.Address));        
                }
            }

            
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

    public class WcfExampleServiceHost : WcfExampleServiceHostBase
    {
        public WcfExampleServiceHost(string address)
            : base(
                typeof (Service.WcfExampleService),
                new[]
                    {
                        new WcfExampleServiceHostServicesModel
                            {
                                Type = typeof (IWcfExampleService),
                                Address = address
                            }
                    })
        {}

        public WcfExampleServiceHost(string address, int port)
            : this( string.Format("{0}:{1}", address, port) )
        { }
    }
}