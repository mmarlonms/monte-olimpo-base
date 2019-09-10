using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MonteOlimpo.Base.CoreException;
using MonteOlimpo.Base.ExceptionHandler.Abstractions;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MonteOlimpo.Base.ExceptionHandler
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<ExceptionHandler>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<int> HandleAsync(Exception exception, HttpContext httpContext)
        {
            if (exception is AggregateException)
            {
                var aggregateException = exception as AggregateException;

                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    await HandleAsync(innerException, httpContext);
                }
            }
            else
            {
                if (exception is UnauthorizedAccessException)
                {
                    _logger.LogInformation("Ocorreu um acesso não autorizado.");

                    var forbidden = new
                    {
                        Key = "Forbidden",
                        Message = "Access to this resource is forbidden."
                    };

                    return await FormatResponseAsync(forbidden, httpContext, 403);
                }

                if (exception is CoreException.CoreException)
                {
                    _logger.LogInformation(exception, "Ocorreu um erro de negócio.");
                    return await FormatResponseAsync(exception, httpContext, 400);
                }
                else
                {
                    Guid logEntryId = Guid.NewGuid();

                    _logger.LogError(exception, "{LogEntryId}: Ocorreu um erro não esperado.", logEntryId);

                    var internalError = new InternalError()
                    {
                        LogEntryId = logEntryId,
                        Exception = (IsDevelopmentEnvironment() ? exception.GetBaseException() : null)
                    };

                    return await FormatResponseAsync(internalError, httpContext, 500);
                }
            }

            return httpContext.Response.StatusCode;
        }

        private bool IsDevelopmentEnvironment()
            => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        private async Task<int> FormatResponseAsync(object output, HttpContext httpContext, int httpStatusCode)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver =
                    new JsonContractResolverForCoreException()
                    {
                        IgnoreSerializableInterface = true
                    },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                MaxDepth = 10,
                Formatting = !IsDevelopmentEnvironment() ? Formatting.None : Formatting.Indented
            };

            var message = JsonConvert.SerializeObject(output, jsonSerializerSettings);

            httpContext.Response.StatusCode = httpStatusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(message, Encoding.UTF8);

            return httpContext.Response.StatusCode;
        }
    }
}
