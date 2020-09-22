using System.Collections.Generic;
using System.Security.Claims;

namespace MonteOlimpo.Identity.Abstractions
{
    public interface IUserPrincipal
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public ICollection<Claim> Claims { get; set; }
        public ICollection<IRolePrincipal> Roles { get;}
    }
}