using System.Collections.Generic;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    public sealed class GetFinishedBattlesQuery : IRequest<IEnumerable<FinishedBattleModel>>
    {
        
    }
}