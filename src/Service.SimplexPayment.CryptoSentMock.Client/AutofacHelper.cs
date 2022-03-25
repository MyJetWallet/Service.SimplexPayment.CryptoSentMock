using Autofac;
using Service.SimplexPayment.CryptoSentMock.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.SimplexPayment.CryptoSentMock.Client
{
    public static class AutofacHelper
    {
        public static void RegisterSimplexPaymentCryptoSentMockClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new SimplexPaymentCryptoSentMockClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
