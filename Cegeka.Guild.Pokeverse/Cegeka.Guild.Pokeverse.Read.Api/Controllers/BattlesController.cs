using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Persistence.Mongo;
using Cegeka.Guild.Pokeverse.Read.Domain.Battle;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Read.Api.Controllers
{
    [Route("api/battles")]
    public class BattlesController : ControllerBase
    {
        private readonly IMongoStorage storage;

        public BattlesController(IMongoStorage storage)
        {
            this.storage = storage;
        }

        [HttpGet("ongoing")]
        public async Task<IActionResult> GetOnGoingBattles()
        {
            var battles = await storage.Find<Battle>(b => b.EndedAt == null);
            return Ok(battles);
        }

        [HttpGet("ended")]
        public async Task<IActionResult> GetEndedBattles()
        {
            var battles = await storage.Find<Battle>(b => b.EndedAt != null);
            return Ok(battles);
        }
    }
}