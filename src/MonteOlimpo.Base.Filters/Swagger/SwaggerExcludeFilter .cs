﻿using Microsoft.OpenApi.Models;
using MonteOlimpo.Base.CoreException;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonteOlimpo.Base.Filters.Swagger
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        private static readonly HashSet<string> ignoreProperties = new HashSet<string>(
            typeof(System.Exception).GetProperties().Where(p => p.Name != nameof(Exception.Message))
                .Select(p => p.Name)
        );

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type != null && schema.Properties != null && schema.Properties.Count > 0)
            {
                if (typeof(Exception).IsAssignableFrom(context.Type))
                {
                    foreach (var ignoreProperty in ignoreProperties)
                    {
                        var keyCheck = schema.Properties
                            .Keys.Where(k => k.ToLowerInvariant() == ignoreProperty.ToLowerInvariant());

                        if (keyCheck.Any() && keyCheck.Count() == 1)
                        {
                            schema.Properties.Remove(keyCheck.Single());
                        }
                    }
                }
                else if (typeof(InternalError).IsAssignableFrom(context.Type))
                {
                    var keyCheck = schema.Properties
                        .Keys.Where(k => k.ToLowerInvariant() == nameof(InternalError.Exception).ToLowerInvariant());

                    if (keyCheck.Any() && keyCheck.Count() == 1)
                    {
                        schema.Properties.Remove(keyCheck.Single());
                    }
                }
            }
        }
    }
}