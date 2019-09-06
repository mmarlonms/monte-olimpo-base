using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonteOlimpo.ApiBoot;
using MonteOlimpo.Sample.WebApi.Exceptions;
using System;
using System.Collections.Generic;

namespace MonteOlimpo.Sample.WebApi.Controllers
{

    public class ValuesController : ApiBaseController
    {
        private readonly ILogger logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            logger.LogError("Logando Values");
            return new string[] { "Josi", "N" };
        }

      
        [HttpGet("ThrowNewCustonException")]
        public ActionResult<string> ThrowNewCustonException()
        {
            throw new CustonException(CustonError.CustonErrorSample);
        }

        [HttpGet("ThrowNewException")]
        public ActionResult<string> ThrowNewException()
        {
            throw new Exception("Erro");
        }
    }
}