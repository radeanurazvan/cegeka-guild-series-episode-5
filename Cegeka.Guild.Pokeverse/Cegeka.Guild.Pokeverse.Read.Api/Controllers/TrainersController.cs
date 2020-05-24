using System;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Persistence.Mongo;
using Cegeka.Guild.Pokeverse.Read.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Read.Api.Controllers
{
    [ApiController]
    [Route("api/trainers")]
    public class TrainersController : ControllerBase
    {
        private readonly IMongoStorage mongoStorage;

        public TrainersController(IMongoStorage mongoStorage)
        {
            this.mongoStorage = mongoStorage;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var trainers = await mongoStorage.GetAll<Trainer>();

            return Ok(trainers);
        }
    }
}
