using System.ServiceModel;
using System.Threading.Tasks;
using Service.SimplexPayment.CryptoSentMock.Grpc.Models;

namespace Service.SimplexPayment.CryptoSentMock.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}