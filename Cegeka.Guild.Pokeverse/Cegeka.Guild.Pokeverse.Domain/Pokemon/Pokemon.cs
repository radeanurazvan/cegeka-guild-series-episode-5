using System;
using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Pokemon : AggregateRoot
    {
        private readonly ICollection<PokemonBattle> battles = new List<PokemonBattle>();

        private Pokemon()
        {
        }

        private Pokemon(Trainer trainer, PokemonDefinition definition) : this()
        {
            DefinitionId = definition.Id;
            TrainerId = trainer.Id;
            Stats = PokemonStats.Default;
            Level = PokemonLevel.Default();
        }

        public static Result<Pokemon> Create(Trainer trainer, PokemonDefinition definition)
        {
            var trainerResult = Result.FailureIf(trainer == null, Messages.InvalidTrainer);
            var definitionResult = Result.FailureIf(definition == null, Messages.InvalidPokemonDefinition);

            return Result.FirstFailureOrSuccess(trainerResult, definitionResult)
                .Map(() => new Pokemon(trainer, definition));
        }

        public Guid TrainerId { get; private set; }

        public PokemonDefinition Definition { get; private set; }

        public Guid DefinitionId { get; private set; }

        public PokemonStats Stats { get; private set; }

        public PokemonLevel Level { get; private set; }

        public string Name => this.Definition.Name;

        public IEnumerable<Ability> Abilities => this.Definition.Abilities.Where(a => a.RequiredLevel <= Level);

        public IEnumerable<PokemonBattle> Battles => this.battles;

        public Result<Battle> Attack(Pokemon other)
        {
            return Result.FailureIf(other == null, Messages.InvalidPokemon)
                .Ensure(() => this != other, Messages.CannotFightItself)
                .Ensure(() => this.TrainerId != other.TrainerId, Messages.SiblingsCannotFight)
                .Ensure(() => !this.IsInBattle, Messages.PokemonAlreadyInBattle)
                .Ensure(() => !other.IsInBattle, Messages.PokemonAlreadyInBattle)
                .Map(() => Battle.Create(this, other))
                .Tap(b => this.battles.Add(new PokemonBattle(this, b)));
        }

        public Result UseAbility(Guid battleId, Guid abilityId)
        {
            var abilityResult = this.Definition.Abilities.FirstOrNothing(a => a.Id == abilityId).ToResult(Messages.UnknownAbility)
                .Ensure(a => a.RequiredLevel <= Level, Messages.CannotUseAbility);
            var battleResult = this.battles.FirstOrNothing(b => b.BattleId == battleId).Select(b => b.Battle).ToResult(Messages.BattleDoesNotExist)
                .Ensure(b => b.IsOnGoing, Messages.BattleHasEnded)
                .Ensure(b => b.ActivePlayer == this.Id, Messages.NotYourTurn);

            return Result.FirstFailureOrSuccess(abilityResult, battleResult)
                .Bind(() => battleResult.Value.TakeTurn(this.Id, abilityResult.Value));
        }

        internal void CollectExperience(int points)
        {
            this.Level = this.Level.WithMoreExperience(points);
            this.AddDomainEvent(new ExperienceGainedEvent(this));
        }

        public Result LevelUp()
        {
            return this.Level.Next()
                .Tap(l => this.Level = l);
        }

        private bool IsInBattle => this.battles.Any(b => b.Battle.IsOnGoing);

        public static class Expressions
        {
            public static string Battles = nameof(battles);
        }
    }
}