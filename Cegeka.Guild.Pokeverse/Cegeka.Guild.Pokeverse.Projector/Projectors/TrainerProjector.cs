using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.Persistence.Mongo;
using Cegeka.Guild.Pokeverse.Read.Domain;
using Trainer = Cegeka.Guild.Pokeverse.Read.Domain.Trainer;
using Pokemon = Cegeka.Guild.Pokeverse.Read.Domain.Pokemon;
using PokemonStats = Cegeka.Guild.Pokeverse.Read.Domain.PokemonStats;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal sealed class TrainerProjector : IMessageHandler<TrainerRegisteredEvent>,
        IMessageHandler<BattleEndedEvent>,
        IMessageHandler<PokemonCreatedEvent>,
        IMessageHandler<PokemonLeveledUpEvent>
    {
        private readonly IMongoStorage mongoStorage;

        public TrainerProjector(IMongoStorage mongoStorage)
        {
            this.mongoStorage = mongoStorage;
        }

        public async Task Handle(TrainerRegisteredEvent @event)
        {
            var duplicateTrainer = await mongoStorage.FindOne<Trainer>(t => t.Id == @event.Id.ToString());
            if (duplicateTrainer.HasValue)
            {
                Console.WriteLine($"Trainer {@event.Id}:{@event.Name} already projected, not projecting anymore");
                return;
            }

            var trainer = new Trainer
            {
                Id = @event.Id.ToString(),
                Name = @event.Name,
                Pokemons = new List<Pokemon>(),
                BattleHistories = new List<BattleHistory>()
            };

            await mongoStorage.Create(trainer);
        }

        public async Task Handle(BattleEndedEvent @event)
        {
            await mongoStorage.Update<Trainer>(@event.WinnerTrainerId.ToString(), trainer =>
            {
                if(trainer.BattleHistories.Any(h => h.BattleId == @event.BattleId.ToString()))
                {
                    Console.WriteLine($"Battle history already exists for battle {@event.BattleId} and trainer {@event.WinnerTrainerId}");
                    return;
                }

                trainer.BattleHistories.Add(new BattleHistory
                {
                    BattleId = @event.BattleId.ToString(),
                    Pokemon = @event.WinnerPokemon,
                    Status = BattleStatus.Won,
                });
            });

            await mongoStorage.Update<Trainer>(@event.LoserTrainerId.ToString(), trainer =>
            {
                if (trainer.BattleHistories.Any(h => h.BattleId == @event.BattleId.ToString()))
                {
                    Console.WriteLine($"Battle history already exists for battle {@event.BattleId} and trainer {@event.LoserTrainerId}");
                    return;
                }

                trainer.BattleHistories.Add(new BattleHistory
                {
                    BattleId = @event.BattleId.ToString(),
                    Pokemon = @event.LoserPokemon,
                    Status = BattleStatus.Lost
                });
            });
        }

        public async Task Handle(PokemonCreatedEvent @event)
        {
            await mongoStorage.Update<Trainer>(@event.TrainerId.ToString(), trainer =>
            {
                if (trainer.Pokemons.Any(p => p.Id == @event.Id.ToString()))
                {
                    Console.WriteLine($"Pokemon {@event.Id}:{@event.Name} already exists for trainer {@event.TrainerId}");
                    return;
                }

                trainer.Pokemons.Add(new Pokemon
                {
                    Id = @event.Id.ToString(),
                    Name = @event.Name,
                    Level = 1,
                    Stats = new PokemonStats
                    {
                        CriticalStrikeChancePoints = @event.CriticalStrikeChancePoints,
                        DamagePoints = @event.DamagePoints,
                        HealthPoints = @event.HealthPoints
                    }
                });
            });
        }

        public Task Handle(PokemonLeveledUpEvent @event)
        {
            return mongoStorage.Update<Trainer>(@event.TrainerId.ToString(), trainer =>
            {
                var pokemon = trainer.Pokemons.FirstOrDefault(x => x.Id == @event.PokemonId.ToString());
                pokemon.Level = @event.Level;
            });
        }
    }
}