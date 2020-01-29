using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MonteOlimpo.Base.ValidationHandler.Abstractions;
using System;

namespace MonteOlimpo.Base.Filters.Validation
{
    public class ValidatorActionFilter : IActionFilter
    {
        private readonly ILogger _logger;
        readonly IValidationHandler _validationHandler;

        public ValidatorActionFilter(IValidationHandler validationHandler, ILoggerFactory loggerFactory)
        {
            _validationHandler = validationHandler ?? throw new ArgumentNullException(nameof(IValidationHandler));
            _logger = loggerFactory?.CreateLogger<ValidatorActionFilter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = _validationHandler.Handle(context);
                }
            }
            catch (System.Exception e)
            {
                _logger.LogCritical(0, e, $"BUG na biblioteca que implementa '{typeof(IValidationHandler).FullName}'. Verifique a exceção no log para obter mais detalhes.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }
    }
}