using System;
using System.Net;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Api.Extensions;
using Cegeka.Guild.Pokeverse.Business;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api
{
    [Route("api/battles")]
    public class BattlesController : ControllerBase
    {
        private readonly IMediator mediator;

        public BattlesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> StartBattle([FromBody] StartBattleModel model)
        {
            var result = await mediator.Send(new StartBattleCommand(model.AttackerId, model.DefenderId));
            return result.ToActionResult(() => StatusCode((int)HttpStatusCode.Created));
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> UseAbility([FromRoute] Guid id, [FromBody] UseAbilityModel model)
        {
            var result = await this.mediator.Send(new UseAbilityCommand(id, model.ParticipantId, model.AbilityId));
            return result.ToActionResult(NoContent);
        }
    }
}