using System.Collections.Generic;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    public sealed class GetOngoingBattlesQuery : IRequest<IEnumerable<OngoingBattleModel>>
    {
        
    }
}