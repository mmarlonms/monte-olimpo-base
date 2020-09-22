using System.Collections.Generic;

namespace MonteOlimpo.Identity.Abstractions
{
    public class BasicRole
    {
        public string Name { get; set; }
        public List<string> Claims { get; set; }
    }
}