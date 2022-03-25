using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.SimplexPayment.CryptoSentMock.Grpc;

namespace Service.SimplexPayment.CryptoSentMock.Client
{
    [UsedImplicitly]
    public class SimplexPaymentCryptoSentMockClientFactory: MyGrpcClientFactory
    {
        public SimplexPaymentCryptoSentMockClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
