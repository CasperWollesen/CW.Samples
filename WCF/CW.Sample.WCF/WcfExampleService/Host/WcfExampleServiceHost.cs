using CW.Sample.WCF.WcfExampleService.Interface;

namespace CW.Sample.WCF.WcfExampleService.Host
{
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