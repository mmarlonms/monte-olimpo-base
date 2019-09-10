using Microsoft.AspNetCore.Mvc;

namespace MonteOlimpo.Base.ApiBoot
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
    }
}