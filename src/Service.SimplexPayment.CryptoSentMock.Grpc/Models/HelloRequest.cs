using System.Runtime.Serialization;

namespace Service.SimplexPayment.CryptoSentMock.Grpc.Models
{
    [DataContract]
    public class HelloRequest
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}