using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CW.Sample.WCF.WcfExampleService
{
    public class WcfExampleServiceConfig : WcfCoreServiceConfig
    {
        public const string Namespace = "http://www.softogteknik.dk/mes";
        public const string ContractName = "WcfExampleService";
        public const string Url = Namespace + "/" + ContractName + "/";
      
        public static string CreateUrl(string host)
        {
            return CreateUrl(host, ContractName);
        }
    }

    public class WcfCoreServiceConfig
    {
        public static Binding CreateBinding()
        {
            var tcpBinding = new NetTcpBinding { TransactionFlow = false };
            tcpBinding.Security.Transport.ProtectionLevel = ProtectionLevel.None;
            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
            tcpBinding.Security.Mode = SecurityMode.None;

            tcpBinding.MaxReceivedMessageSize = int.MaxValue;
            tcpBinding.MaxBufferSize = int.MaxValue;

            return tcpBinding;
        }    
        
        public static string CreateUrl(string host, string contractName)
        {
            return "net.tcp://" + host + "/" + contractName + "/";
        }
    }


}
