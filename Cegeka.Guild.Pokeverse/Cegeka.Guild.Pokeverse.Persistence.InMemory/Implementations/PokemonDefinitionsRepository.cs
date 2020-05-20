using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Persistence.InMemory
{
    internal class PokemonDefinitionsRepository : IReadRepository<PokemonDefinition>, IWriteRepository<PokemonDefinition>
    {
        private readonly ICollection<PokemonDefinition> definitions = new List<PokemonDefinition>
        {
            new PokemonDefinition
            {
                Name = "Pikachu",
                Abilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Scratch",
                        Damage = 2,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Tail Whip",
                        Damage = 3,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Lightning Strike",
                        Damage = 12,
                        RequiredLevel = 3
                    }
                }
            },
            new PokemonDefinition
            {
                Name = "Squirtle",
                Abilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Scratch",
                        Damage = 2,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Tail Whip",
                        Damage = 3,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Water Jet",
                        Damage = 12,
                        RequiredLevel = 3
                    }
                }
            },
            new PokemonDefinition
            {
                Name = "Bulbasaur",
                Abilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Scratch",
                        Damage = 2,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Tail Whip",
                        Damage = 3,
                        RequiredLevel = 1
                    },
                    new Ability
                    {
                        Name = "Leaves strike",
                        Damage = 12,
                        RequiredLevel = 3
                    }
                }
            }
        };

        public Task<IEnumerable<PokemonDefinition>> GetAll() => Task.FromResult<IEnumerable<PokemonDefinition>>(definitions);

        public Task<Maybe<PokemonDefinition>> GetById(Guid id) => Task.FromResult(definitions.FirstOrNothing(p => p.Id == id));

        public Task Add(PokemonDefinition entity)
        {
            definitions.Add(entity);
            return Task.CompletedTask;
        }

        public Task Save() => Task.CompletedTask;
    }
}