using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace MonteOlimpo.Base.Crosscutting.Swagger
{
    internal class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters.OfType<OpenApiParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                    parameter.Description = description.ModelMetadata?.Description;

                if (routeInfo == null)
                    continue;

               
                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}