using System;
using System.ServiceModel;
using CW.Sample.WCF.WcfExampleService.Config;

namespace CW.Sample.WCF.WcfExampleService.Host
{
    public class WcfExampleServiceHostBase : IDisposable
    {
        protected ServiceHost Host;

        private readonly Type _serviceFacadeType;
        private readonly WcfExampleServiceHostServicesModel[] _serviceRelations;

        public WcfExampleServiceHostBase(Type serviceFacadeType, WcfExampleServiceHostServicesModel[] serviceRelations)
        {
            _serviceFacadeType = serviceFacadeType;
            _serviceRelations = serviceRelations;
        }

        public void Start()
        {
            Host = new ServiceHost(_serviceFacadeType);

            if (_serviceRelations != null)
            {
                foreach (WcfExampleServiceHostServicesModel relation in _serviceRelations)
                {
                    Host.AddServiceEndpoint(relation.Type, WcfExampleServiceConfig.CreateBinding(), WcfExampleServiceConfig.CreateUrl(relation.Address));        
                }
            }

            
            Host.Open();
        }

        public void Close()
        {
            if (Host != null)
            {
                Host.Close();
            }
        }

        public void Dispose()
        {
            if (Host != null)
            {
                Close();
                var disposer = Host as IDisposable;
                disposer.Dispose();
            }
        }
    }
}