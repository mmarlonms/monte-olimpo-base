namespace MonteOlimpo.Identity.Abstractions
{
    public interface IUserPrincipalTokenizer
    {
        GenerateTokenResult GenerateToken(IUserPrincipal userPrincipal);

        GenerateTokenResult GenerateToken(IUserPrincipal userPrincipal, string role);

        bool ValidateToken(string authToken, bool validateTime);
    }
}