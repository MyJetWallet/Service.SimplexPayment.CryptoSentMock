using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.ServiceBus;
using MyJetWallet.Sdk.WalletApi.Wallets;
using MyServiceBus.Abstractions;
using Service.Blockchain.Wallets.MyNoSql.AssetsMappings;
using Service.ClientWallets.Client;
using Service.Fireblocks.Webhook.ServiceBus.Deposits;
using Service.SimplexPayment.CryptoSentMock.Jobs;
using Service.SimplexPayment.Domain.Models;

namespace Service.SimplexPayment.CryptoSentMock.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient =
                builder.RegisterMyServiceBusTcpClient((() => Program.Settings.SpotServiceBusHostPort), Program.LogFactory);
            var queueName = "Service.SimplexPayment.CryptoSentMock";
            
            builder.RegisterMyServiceBusSubscriberSingle<SimplexIntention>(serviceBusClient,
                SimplexIntention.TopicName, queueName, TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusPublisher<FireblocksDepositSignal>(serviceBusClient, Fireblocks.Webhook.ServiceBus.Topics.FireblocksDepositSignalTopic, false);

            var myNoSqlClient = builder.CreateNoSqlClient(() => Program.Settings.MyNoSqlReaderHostPort);
            builder.RegisterMyNoSqlReader<AssetMappingNoSql>(myNoSqlClient, AssetMappingNoSql.TableName);
            builder.RegisterClientWalletsClients(myNoSqlClient, Program.Settings.ClientWalletsGrpcServiceUrl);

            builder
                .RegisterType<WalletService>()
                .As<IWalletService>()
                .SingleInstance();
            
            builder
                .RegisterType<MockPaymentJob>()
                .AsSelf()
                .SingleInstance()
                .AutoActivate();
        }
    }
}