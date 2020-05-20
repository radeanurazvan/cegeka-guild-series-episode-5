using System;
using Cegeka.Guild.Pokeverse.Business;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class Battle : AggregateRoot
    {
        private Battle()
        {
            StartedAt = DateTime.Now;
        }

        internal static Battle Create(Pokemon attacker, Pokemon defender)
        {
            return new Battle
            {
                ActivePlayer = attacker.Id,
                AttackerId =  attacker.Id,
                Attacker = new PokemonInFight(attacker),
                DefenderId = defender.Id,
                Defender = new PokemonInFight(defender)
            };
        }

        public Guid AttackerId { get; private set; }

        public PokemonInFight Attacker { get; private set; }

        public Guid DefenderId { get; private set; }

        public PokemonInFight Defender { get; private set; }

        public Guid ActivePlayer { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime FinishedAt { get; private set; }

        public Pokemon Winner { get; private set; }

        public Guid? WinnerId { get; private set; }

        public Pokemon Loser { get; private set; }

        public Guid? LoserId { get; private set; }

        public bool IsOnGoing => Winner == null;

        internal Result TakeTurn(Guid player, Ability ability)
        {
            return Result.SuccessIf(IsOnGoing, Messages.BattleHasEnded)
                .Map(() => AttackerId == player ? Defender : Attacker)
                .Tap(victim => victim.TakeDamage(ability.Damage))
                .Tap(victim => ActivePlayer = victim.PokemonId)
                .Tap(TryConcludeBattle);
        }

        public Result AwardParticipants()
        {
            return Result.FailureIf(IsOnGoing, Messages.BattleIsOngoing)
                .Tap(() => this.Loser.CollectExperience(BattleExperience.ForLoser()))
                .Tap(() => this.Winner.CollectExperience(BattleExperience.ForWinner()));
        }

        private void TryConcludeBattle(PokemonInFight victim)
        {
            if (victim.Health > 0)
            {
                return;
            }

            this.Loser = victim.Pokemon;
            this.Winner = victim.PokemonId == AttackerId ? Defender.Pokemon : Attacker.Pokemon;
            this.FinishedAt = DateTime.Now;
            this.AddDomainEvent(new BattleEndedEvent(this));
        }
    }
}