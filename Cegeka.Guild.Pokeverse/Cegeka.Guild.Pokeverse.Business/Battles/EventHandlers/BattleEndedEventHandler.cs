﻿using System;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class BattleEndedEventHandler : IMessageHandler<BattleEndedEvent>
    {
        private readonly IRepositoryMediator mediator;

        public BattleEndedEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task Handle(BattleEndedEvent @event)
        {
            var battleResult = await this.mediator.Read<Battle>().GetById(@event.BattleId).ToResult(Messages.BattleDoesNotExist);

            await battleResult
                .Tap(b => Award(b, b.Attacker.Id))
                .Tap(b => Award(b, b.Defender.Id));
        }

        private Task<Result> Award(Battle battle, Guid pokemonId)
        {
            return mediator.Read<Pokemon>().GetById(pokemonId).ToResult(Messages.PokemonDoesNotExist)
                .Bind(battle.Award);
        }
    }
}