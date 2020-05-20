using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    public class UseAbilityCommand : IRequest<Result>
    {
        public UseAbilityCommand(Guid battleId, Guid participantId, Guid abilityId)
        {
            BattleId = battleId;
            ParticipantId = participantId;
            AbilityId = abilityId;
        }
        public Guid BattleId { get; }
        
        public Guid ParticipantId { get; }
        
        public Guid AbilityId { get; }
    }
}