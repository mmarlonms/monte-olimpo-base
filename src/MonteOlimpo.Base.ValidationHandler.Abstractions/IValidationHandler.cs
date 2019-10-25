using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MonteOlimpo.Base.ValidationHandler.Abstractions
{
    public interface IValidationHandler
    {
        ObjectResult Handle(ActionExecutingContext filterContext);
    }
}