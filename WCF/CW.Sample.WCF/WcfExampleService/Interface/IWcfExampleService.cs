using System.ServiceModel;
using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Models;

namespace CW.Sample.WCF.WcfExampleService.Interface
{
    /// <summary>
    /// This interface is created for demostration the difference possibilites to create a WcfService with sync and async support.
    /// </summary>
    [ServiceContract(Name = WcfExampleServiceConfig.ContractName, Namespace = WcfExampleServiceConfig.Namespace)]
    public interface IWcfExampleService
    {
        /// <summary>
        /// Simple sync method which passes some values.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        SimpleSyncMethodResponseModel SimpleSyncMethod(SimpleSyncMethodRequestModel request);

        [OperationContract]
        Task<SimpleTaskMethodResponseModel> SimpleTaskMethod(SimpleTaskMethodRequestModel request);
    }

    [ServiceContract(Name = WcfExampleServiceConfig.ContractName, Namespace = WcfExampleServiceConfig.Namespace)]
    public interface IWcfExampleServiceAsync : IWcfExampleService
    {
        /// <summary>
        /// Just copy paste method and attributes and change return value to Task<return value>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        Task<SimpleSyncMethodResponseModel> SimpleSyncMethodAsync(SimpleSyncMethodRequestModel request);
    }

 
}