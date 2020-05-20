using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Trainer : AggregateRoot
    {
        private readonly ICollection<Pokemon> pokemons = new List<Pokemon>();

        private Trainer()
        {
        }

        private Trainer(string name)
        {
            Name = name;
        }

        public static Result<Trainer> Create(string name)
        {
            return Result.FailureIf(string.IsNullOrEmpty(name), name, Messages.InvalidTrainerName)
                .Map(n => new Trainer(n))
                .Tap(t => t.AddDomainEvent(new TrainerRegisteredEvent(t)));
        }

        public string Name { get; private set; }

        public IEnumerable<Pokemon> Pokemons => this.pokemons;

        public static class Expressions
        {
            public const string Pokemons = nameof(Pokemons);
        }
    }
}