using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.SimplexPayment.CryptoSentMock.Settings
{
    public class SettingsModel
    {
        [YamlProperty("SimplexPaymentCryptoSentMock.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("SimplexPaymentCryptoSentMock.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("SimplexPaymentCryptoSentMock.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
        
        [YamlProperty("SimplexPaymentCryptoSentMock.DefaultBroker")]
        public string DefaultBroker { get; set; }
        
        [YamlProperty("SimplexPaymentCryptoSentMock.DefaultBrand")]
        public string DefaultBrand { get; set; }
        
        [YamlProperty("SimplexPaymentCryptoSentMock.ClientWalletsGrpcServiceUrl")]
        public string ClientWalletsGrpcServiceUrl { get; set; }

        [YamlProperty("SimplexPaymentCryptoSentMock.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }
        
        [YamlProperty("SimplexPaymentCryptoSentMock.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }

        [YamlProperty("SimplexPaymentCryptoSentMock.DelayInSec")]
        public int DelayInSec { get; set; }
    }
}
