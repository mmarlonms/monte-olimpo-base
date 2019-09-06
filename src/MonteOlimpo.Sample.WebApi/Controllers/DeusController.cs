using Microsoft.AspNetCore.Mvc;
using MonteOlimpo.ApiBoot;
using MonteOlimpo.Domain.Models;
using MonteOlimpo.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MonteOlimpo.Sample.WebApi.Controllers
{
    public class DeusController : ApiBaseController
    {
        private readonly IDeusRepository deusRepository;

        public DeusController(IDeusRepository deusRepository)
        {
            this.deusRepository = deusRepository;
        }

        [HttpGet("ObterTodosOsDeusesDoMonteOlimpo")]
        public List<Deus> ObterTodosOsDeusesDoMonteOlimpo()
        {
            return this.deusRepository.List().ToList();
        }

        [HttpPost("CriarNovoDeus")]
        public void CriarNovoDeusDoMonteOlimpo(Deus deus)
        {
            this.deusRepository.Add(deus);
        }

        [HttpPut("ModificarDeusDoMonteOlimpo")]
        public void ModificarDeusDoMonteOlimpo(Deus deus)
        {
            this.deusRepository.Update(deus);
        }
    }
}