using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class TrainerRegisteredEventHandler : INotificationHandler<TrainerRegisteredEvent>
    {
        private const int RandomPokemonsOnRegister = 2;

        private readonly IReadRepository<PokemonDefinition> definitionsReadRepository;
        private readonly IReadRepository<Trainer> trainersReadRepository;
        private readonly IWriteRepository<Pokemon> pokemonWriteRepository;

        public TrainerRegisteredEventHandler(
            IReadRepository<PokemonDefinition> definitionsReadRepository, 
            IReadRepository<Trainer> trainersReadRepository,
            IWriteRepository<Pokemon> pokemonWriteRepository)
        {
            this.definitionsReadRepository = definitionsReadRepository;
            this.trainersReadRepository = trainersReadRepository;
            this.pokemonWriteRepository = pokemonWriteRepository;
        }

        public async Task Handle(TrainerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var trainer = (await this.trainersReadRepository.GetById(notification.Id)).Value;

            var random = new Random(DateTime.Now.Millisecond);
            var pokemons = await this.definitionsReadRepository.GetAll();
            Enumerable.Range(1, RandomPokemonsOnRegister)
                .Select(_ => random.Next(0, pokemons.Count()))
                .Select(randomIndex => pokemons.ElementAt(randomIndex))
                .Select(definition => Pokemon.Create(trainer, definition))
                .Where(r => r.IsSuccess)
                .Select(r => r.Value)
                .ToList()
                .ForEach(p => pokemonWriteRepository.Add(p));

            await pokemonWriteRepository.Save();
        }
    }
}