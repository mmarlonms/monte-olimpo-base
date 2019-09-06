using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonteOlimpo.ExceptionHandler
{
    internal class JsonContractResolverForCoreException : CamelCasePropertyNamesContractResolver
    {
        private static readonly HashSet<string> _ignoreProperties = new HashSet<string>(
            typeof(Exception).GetProperties().Where(p => p.Name != nameof(Exception.Message))
                .Select(p => p.Name)
        );

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (_ignoreProperties.Contains(member.Name))
            {
                property.Ignored = true;
            }

            return property;
        }
    }
}