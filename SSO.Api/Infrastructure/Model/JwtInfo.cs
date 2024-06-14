namespace SSO.Api.Infrastructure.Model
{
    public class JwtInfo
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
