namespace Estimatz.Entities.AppConfig
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Emissor { get; set; }
        public string Validate { get; set; }
    }
}
