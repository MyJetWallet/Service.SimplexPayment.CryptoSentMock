using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyJetWallet.Domain;
using MyJetWallet.Sdk.ServiceBus;
using MyJetWallet.Sdk.WalletApi.Wallets;
using MyNoSqlServer.Abstractions;
using MyServiceBus.Abstractions;
using Service.Blockchain.Wallets.MyNoSql.AssetsMappings;
using Service.Fireblocks.Webhook.Domain.Models.Deposits;
using Service.Fireblocks.Webhook.ServiceBus.Deposits;
using Service.SimplexPayment.Domain.Models;

namespace Service.SimplexPayment.CryptoSentMock.Jobs
{
    public class MockPaymentJob
    {
        private readonly IServiceBusPublisher<FireblocksDepositSignal> _publisher;
        private readonly IMyNoSqlServerDataReader<AssetMappingNoSql> _assetMappingNoSql;
        private readonly IWalletService _walletService;

        public MockPaymentJob(ISubscriber<SimplexIntention> subscriber, 
            IServiceBusPublisher<FireblocksDepositSignal> publisher, 
            IMyNoSqlServerDataReader<AssetMappingNoSql> assetMappingNoSql,
            IWalletService walletService)
        {
            _publisher = publisher;
            _assetMappingNoSql = assetMappingNoSql;
            _walletService = walletService;
            subscriber.Subscribe(HandleEvent);
        }

        private async ValueTask HandleEvent(SimplexIntention intention)
        {
            if(intention.Status != SimplexStatus.CryptoSent)
                return;
            
            if (string.IsNullOrWhiteSpace(intention.BlockchainTxHash))
                return;

            var wallet = await _walletService.GetDefaultWalletAsync(new JetClientIdentity
            {
                ClientId = intention.ClientId,
                BrandId = Program.Settings.DefaultBrand,
                BrokerId = Program.Settings.DefaultBroker
            });

            var mapping = _assetMappingNoSql.Get().FirstOrDefault(x => x.AssetMapping.FireblocksAssetId == intention.ToAsset);
            var network = mapping.AssetMapping.NetworkId;
            
            var task = _publisher.PublishAsync(new FireblocksDepositSignal
            {
                BrokerId = Program.Settings.DefaultBroker,
                ClientId = intention.ClientId,
                WalletId = wallet.WalletId,
                TransactionId = intention.BlockchainTxHash,
                Amount = intention.ToAmount,
                AssetSymbol = intention.ToAsset,
                Status = FireblocksDepositStatus.New,
                EventDate = DateTime.UtcNow,
                Network = network
            });
            
            Execute(task).Start();
        }

        private async Task Execute(Task publish)
        {
            await Task.Delay(Program.Settings.DelayInSec);
            await publish;
        }
    }
}