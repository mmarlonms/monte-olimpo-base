using System;

namespace MonteOlimpo.Identity.Abstractions
{
    public interface IUserPrincipalBuilder
    {
        IUserPrincipal UserPrincipal { get; }
        string GetAccessToken();
        Guid GetCurrentClientId();
    }
}