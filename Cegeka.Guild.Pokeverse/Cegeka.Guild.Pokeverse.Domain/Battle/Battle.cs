using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Business;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class Battle : AggregateRoot
    {
        private readonly ISet<Guid> rewardedParticipants = new HashSet<Guid>();

        private Battle()
        {
            StartedAt = DateTime.Now;
        }

        internal static Battle Create(Pokemon attacker, Pokemon defender)
        {
            return new Battle
            {
                ActivePlayer = attacker.Id,
                Attacker =  new PokemonInFight(attacker),
                Defender = new PokemonInFight(defender),
            };
        }

        public PokemonInFight Attacker { get; private set; }

        public PokemonInFight Defender { get; private set; }

        public Guid ActivePlayer { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Maybe<DateTime> FinishedAt { get; private set; }

        public Maybe<Guid> WinnerId { get; private set; }

        public Maybe<Guid> LoserId { get; private set; }

        public bool IsOnGoing => FinishedAt.HasNoValue;

        public Result TakeTurn(Guid player, Ability ability)
        {
            return Result.SuccessIf(IsOnGoing, Messages.BattleHasEnded)
                .Ensure(() => ActivePlayer == player, Messages.NotYourTurn)
                .Map(() => Attacker.Id == player ? Defender : Attacker)
                .Tap(victim => victim.TakeDamage(ability.Damage))
                .Tap(victim => ActivePlayer = victim.Id)
                .TapIf(Attacker.Fainted || Defender.Fainted, ConcludeBattle);
        }

        public Result Award(Pokemon pokemon)
        {
            return Result.FailureIf(IsOnGoing, Messages.BattleIsOngoing)
                .Ensure(() => pokemon.Id == WinnerId || pokemon.Id == LoserId, Messages.InvalidPokemon)
                .Ensure(() => !rewardedParticipants.Contains(pokemon.Id), Messages.PokemonAlreadyRewarded)
                .Map(() => pokemon.Id == LoserId ? BattleExperience.ForLoser() : BattleExperience.ForWinner())
                .Tap(pokemon.CollectExperience);
        }

        private void ConcludeBattle(PokemonInFight victim)
        {
            this.LoserId = victim.Id;
            this.WinnerId = victim.Id == Attacker.Id ? Defender.Id : Attacker.Id;

            this.FinishedAt = DateTime.Now;
            this.AddDomainEvent(new BattleEndedEvent(this));
        }
    }
}