using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api
{
    [Route("api/arena")]
    public class ArenaController : ControllerBase
    {
        private readonly IMediator mediator;

        public ArenaController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("ongoing")]
        public async Task<IActionResult> GetOngoingBattles()
        {
            var battles = await this.mediator.Send(new GetOngoingBattlesQuery());
            return Ok(battles);
        }


        [HttpGet("finished")]
        public async Task<IActionResult> GetFinishedBattles()
        {
            var battles = await this.mediator.Send(new GetFinishedBattlesQuery());
            return Ok(battles);
        }
    }
}