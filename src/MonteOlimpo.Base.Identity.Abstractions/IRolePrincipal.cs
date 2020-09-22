using System.Collections.Generic;
using System.Security.Claims;

namespace MonteOlimpo.Identity.Abstractions
{
    public interface IRolePrincipal
    {
        public string Name { get; set; }
        public ICollection<Claim> Claims { get; }
    }
}