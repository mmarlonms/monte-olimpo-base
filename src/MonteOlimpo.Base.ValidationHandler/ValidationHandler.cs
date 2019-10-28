using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MonteOlimpo.Base.CoreException;
using MonteOlimpo.Base.ValidationHandler.Abstractions;
using System;
using System.Collections.Generic;

namespace MonteOlimpo.Base.ValidationHandler
{
    public class ValidationHandler : IValidationHandler
    {
        private readonly ILogger _logger;

        public ValidationHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<ValidationHandler>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public ObjectResult Handle(ActionExecutingContext filterContext)
        {
            _logger.LogInformation("Ouve uma invalidação de uma model");

            var errors = new List<ModelValidationError>();
            foreach (var errorKey in filterContext.ModelState.Keys)
            {
                foreach (var error in filterContext.ModelState[errorKey].Errors)
                {
                    errors.Add(new ModelValidationError(errorKey, error.ErrorMessage));
                }
            }

            var result = new ObjectResult(new { Errors = errors })
            {
                StatusCode = 420
            };
            return result;
        }
    }
}