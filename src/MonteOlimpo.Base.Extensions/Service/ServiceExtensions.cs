using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonteOlimpo.Base.Extensions.Service
{
    public static class ServiceExtensions
    {
        public static void RegisterAllTypes(this IServiceCollection services, IEnumerable<Assembly> assemblies,
           ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            foreach (var implementationType in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => !type.GetTypeInfo().IsAbstract))
            {
                foreach (var interfaceType in implementationType.GetInterfaces())
                {
                    if (DefineTypeToIoC(implementationType.ToString()))
                        services.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));
                }
            }
        }

        private static bool DefineTypeToIoC(string type)
        {
            return type.EndsWith("Service") || type.EndsWith("Repository") || type.EndsWith("Adapter");
        }
    }
}