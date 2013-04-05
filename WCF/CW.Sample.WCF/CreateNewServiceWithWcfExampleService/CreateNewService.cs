// Demonstration of how simple it is to create a new WCF Service with host and client with async support - uses net tcp for transport and requires now xml configuration.
// This is with reuse of same interface and data model, with strong typing - for get build errors on wrong implementation.
 
using System.ServiceModel;
using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Client;
using CW.Sample.WCF.WcfExampleService.Config;
using CW.Sample.WCF.WcfExampleService.Host;
using NUnit.Framework;

namespace CW.Sample.WCF.CreateNewServiceWithWcfExampleService
{
    [TestFixture]
    public class CreateNewServiceUnitTest
    {
        [Test]
        public void StartHostAndClient_SimpleSyncMethodSyncCall_Success()
        {
            using (var host = new CreateNewServiceHost("localhost:10000"))
            {
                host.Start();

                var client = new CreateNewServiceAsyncClient("localhost:10000");
                CreateNewServiceResponseModel responseSimpleSyncMethod = client.SimpleSyncMethod(new CreateNewServiceRequestModel { Message = "Hello World" });
                Assert.IsNotNull(responseSimpleSyncMethod);
                Assert.AreEqual("Hello World", responseSimpleSyncMethod.Message);
                host.Close();
            }
        }
    }

    [ServiceContract(Name = CreateNewServiceConfig.ContractName, Namespace = CreateNewServiceConfig.Namespace)]
    public interface ICreateNewService
    {
        [OperationContract]
        CreateNewServiceResponseModel SimpleSyncMethod(CreateNewServiceRequestModel request);
    }

    [ServiceContract(Name = CreateNewServiceConfig.ContractName, Namespace = CreateNewServiceConfig.Namespace)]
    public interface ICreateNewServiceAsync : ICreateNewService
    {
        [OperationContract]
        Task<CreateNewServiceResponseModel> SimpleSyncMethodAsync(CreateNewServiceRequestModel request);
    }
    
    public class CreateNewService : ICreateNewService
    {
        public CreateNewServiceResponseModel SimpleSyncMethod(CreateNewServiceRequestModel request)
        {
            return new CreateNewServiceResponseModel { Message = request.Message };
        }
    }

    public class CreateNewServiceAsyncClient : WcfExampleServiceClientBase<ICreateNewServiceAsync>, ICreateNewServiceAsync
    {
        public CreateNewServiceAsyncClient(string address)
            : base(address)
        {
        }

        public CreateNewServiceAsyncClient(string address, int port)
            : base(address, port)
        {
        }

        public CreateNewServiceResponseModel SimpleSyncMethod(CreateNewServiceRequestModel request)
        {
            return Channel.SimpleSyncMethod(request);
        }

        public Task<CreateNewServiceResponseModel> SimpleSyncMethodAsync(CreateNewServiceRequestModel request)
        {
            return Channel.SimpleSyncMethodAsync(request);
        }
    }

    public class CreateNewServiceConfig : WcfCoreServiceConfig
    {
        public const string Namespace = "http://www.softogteknik.dk/mes";
        public const string ContractName = "CreateNewService";
        
        public static string CreateUrl(string host)
        {
            return CreateUrl(host, ContractName);
        }

        public static EndpointAddress CreateEndpointAddress(string address)
        {
            return new EndpointAddress(CreateUrl(address));
        }

        public static EndpointAddress CreateEndpointAddress(string address, int port)
        {
            return new EndpointAddress(CreateUrl(string.Format("{0}:{1}", address, port)));
        }
    }
    
    public class CreateNewServiceHost : WcfExampleServiceHostBase
    {
        public CreateNewServiceHost(string address)
            : base(
                typeof(CreateNewService),
                new[]
                    {
                        new WcfExampleServiceHostServicesModel
                            {
                                Type = typeof (ICreateNewService),
                                Address = address
                            }
                    })
        { }

        public CreateNewServiceHost(string address, int port)
            : this(string.Format("{0}:{1}", address, port))
        { }
    }
    
    public class CreateNewServiceRequestModel
    {
        public string Message { get; set; }
    }

    public class CreateNewServiceResponseModel
    {
        public string Message { get; set; }
    }
}
