using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.SimplexPayment.CryptoSentMock.Settings
{
    public class SettingsModel
    {
        [YamlProperty("SimplexPayment.CryptoSentMock.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("SimplexPayment.CryptoSentMock.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("SimplexPayment.CryptoSentMock.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
    }
}
