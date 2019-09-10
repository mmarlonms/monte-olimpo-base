using System.Collections.Generic;

namespace MonteOlimpo.Base.Crosscutting.Swagger
{
    public class SwaggerConfiguration
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultVersion { get; set; }
        public IDictionary<string, string> VersionIngDescriptions { get; set; }
    }
}