using Microsoft.AspNetCore.Mvc;

namespace MonteOlimpo.Base.ApiBoot
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
    }
}