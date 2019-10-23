namespace MonteOlimpo.Base.Authentication
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public string[] Roles { get; set; }
        public string Secret { get; set; }
    }
}