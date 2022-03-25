using System.Runtime.Serialization;
using Service.SimplexPayment.CryptoSentMock.Domain.Models;

namespace Service.SimplexPayment.CryptoSentMock.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}