namespace ProductsStore.Application.ConfigurationTemplates
{
    public class JwtConfiguration
    {
        public required string Key { get; set; }
        public int ExpirationInHours { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }

    }
}
