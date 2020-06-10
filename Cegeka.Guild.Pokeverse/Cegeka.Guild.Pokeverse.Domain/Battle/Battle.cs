using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed partial class Battle : AggregateRoot
    {
        private readonly ISet<Guid> rewardedParticipants = new HashSet<Guid>();

        private Battle()
        {
        }

        private Battle(Pokemon attacker, Pokemon defender)
            : this()
        {
            ReactToDomainEvent(new BattleStartedEvent(this, attacker, defender));
        }

        internal static Result<Battle> Create(Pokemon attacker, Pokemon defender)
        {
            var attackerResult = Maybe<Pokemon>.From(attacker).ToResult(Messages.InvalidPokemon);
            var defenderResult = Maybe<Pokemon>.From(defender).ToResult(Messages.InvalidPokemon);

            return Result.FirstFailureOrSuccess(attackerResult, defenderResult)
                .Map(() => new Battle(attacker, defender));
        }

        public PokemonInFight Attacker { get; private set; }

        public PokemonInFight Defender { get; private set; }

        public Guid ActivePlayer { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Maybe<DateTime> FinishedAt { get; private set; } = Maybe<DateTime>.None;

        public Maybe<PokemonInFight> Winner => Attacker.Fainted ? Defender : Defender.Fainted ? Attacker : Maybe<PokemonInFight>.None;

        public Maybe<Guid> WinnerId => Winner.Select(w => w.Id);

        public Maybe<PokemonInFight> Loser => Attacker.Fainted ? Attacker : Defender.Fainted ? Defender : Maybe<PokemonInFight>.None;
        
        public Maybe<Guid> LoserId => Loser.Select(l => l.Id);

        public bool IsOnGoing => FinishedAt.HasNoValue;

        public Result TakeTurn(Guid player, Ability ability)
        {
            return Result.SuccessIf(IsOnGoing, Messages.BattleHasEnded)
                .Ensure(() => ActivePlayer == player, Messages.NotYourTurn)
                .Tap(() => ReactToDomainEvent(new PlayerTookTurnEvent(this, player, ability)))
                .Tap(ConcludeBattle);
        }

        public Result Award(Pokemon pokemon)
        {
            return Result.FailureIf(IsOnGoing, Messages.BattleIsOngoing)
                .Ensure(() => pokemon.Id == WinnerId || pokemon.Id == LoserId, Messages.InvalidPokemon)
                .Ensure(() => !rewardedParticipants.Contains(pokemon.Id), Messages.PokemonAlreadyRewarded)
                .Map(() => pokemon.Id == LoserId ? BattleExperience.ForLoser() : BattleExperience.ForWinner())
                .Tap(experiencePoints => ReactToDomainEvent(new PlayerAwardedEvent(this, pokemon.Id, experiencePoints)));
        }

        private Result ConcludeBattle()
        {
            return Result.SuccessIf(Attacker.Fainted || Defender.Fainted, Messages.BattleIsOngoing)
                .Tap(() => ReactToDomainEvent(new BattleEndedEvent(this)));
        }
    }
}