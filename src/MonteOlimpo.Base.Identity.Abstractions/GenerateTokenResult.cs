using System;

namespace MonteOlimpo.Identity.Abstractions
{
    public class GenerateTokenResult
    {
        public GenerateTokenResult(string accessToken, DateTime? expiration)
        {
            AccessToken = accessToken;
            Expiration = expiration;
        }

        public string AccessToken { get; }
        public DateTime? Expiration { get; }
    }
}