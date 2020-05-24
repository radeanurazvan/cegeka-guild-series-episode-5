using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business;
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

        public Task Handle(TrainerRegisteredEvent @event)
        {
            var trainer = new Trainer
            {
                Id = @event.Id.ToString(),
                Name = @event.Name,
                Pokemons = new List<Pokemon>(),
                BattleHistories = new List<BattleHistory>()
            };

            return mongoStorage.Create(trainer);
        }

        public async Task Handle(BattleEndedEvent @event)
        {
            await mongoStorage.Update<Trainer>(@event.WinnerTrainerId.ToString(), trainer =>
            {
                trainer.BattleHistories.Add(new BattleHistory
                {
                    BattleId = @event.BattleId.ToString(),
                    Pokemon = @event.WinnerPokemon,
                    Status = BattleStatus.Won,
                });
            });

            await mongoStorage.Update<Trainer>(@event.LoserTrainerId.ToString(), trainer =>
            {
                trainer.BattleHistories.Add(new BattleHistory
                {
                    BattleId = @event.BattleId.ToString(),
                    Pokemon = @event.LoserPokemon,
                    Status = BattleStatus.Lost
                });
            });
        }

        public Task Handle(PokemonCreatedEvent @event)
        {
            return mongoStorage.Update<Trainer>(@event.TrainerId.ToString(), trainer =>
            {
                trainer.Pokemons.Add(new Pokemon
                {
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