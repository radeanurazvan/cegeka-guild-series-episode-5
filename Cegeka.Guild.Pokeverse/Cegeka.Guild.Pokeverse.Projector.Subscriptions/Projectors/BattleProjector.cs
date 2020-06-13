using System;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.Persistence.Mongo;
using Cegeka.Guild.Pokeverse.Read.Domain.Battle;
using Newtonsoft.Json;
using Battle = Cegeka.Guild.Pokeverse.Read.Domain.Battle.Battle;

namespace Cegeka.Guild.Pokeverse.Projector.Subscriptions.Projectors
{
    internal sealed class BattleProjector : IEventHandler<BattleStartedEvent>,
        IEventHandler<PlayerTookTurnEvent>,
        IEventHandler<BattleEndedEvent>,
        IEventHandler<PlayerAwardedEvent>
    {
        private readonly IMongoStorage storage;

        public BattleProjector(IMongoStorage storage)
        {
            this.storage = storage;
        }

        public Task Handle(BattleStartedEvent @event)
        {
            LogAboutEvent(@event);

            var battle = new Battle
            {
                Id = @event.Id.ToString(),
                Attacker = new PokemonInBattle
                {
                    Id = @event.Attacker.Id.ToString(),
                    Name = @event.Attacker.Name,
                    Health = @event.Attacker.Health
                },
                Defender = new PokemonInBattle
                {
                    Id = @event.Defender.Id.ToString(),
                    Name = @event.Defender.Name,
                    Health = @event.Defender.Health
                },
                StartedAt = @event.StartedAt
            };

            return storage.Create(battle);
        }

        public async Task Handle(PlayerTookTurnEvent @event)
        {
            LogAboutEvent(@event);

            var battle = (await storage.FindOne<Battle>(b => b.Id == @event.BattleId.ToString())).Value;
            var playerName = battle.Attacker.Id == @event.PlayerId.ToString()
                ? battle.Attacker.Name
                : battle.Defender.Name;

            var comment = $"{playerName} used {@event.Ability.Name} and dealt {@event.Ability.Damage} damage!";
            battle.Comments.Add(comment);

            await storage.Update(battle);
        }

        public Task Handle(BattleEndedEvent @event)
        {
            return storage.Update<Battle>(@event.BattleId.ToString(), b => b.EndedAt = @event.EndedAt);
        }

        public async Task Handle(PlayerAwardedEvent @event)
        {
            LogAboutEvent(@event);

            var battle = (await storage.FindOne<Battle>(b => b.Id == @event.BattleId.ToString())).Value;
            var playerName = battle.Attacker.Id == @event.PlayerId.ToString()
                ? battle.Attacker.Name
                : battle.Defender.Name;

            var comment = $"{playerName} has been awarded with {@event.ExperiencePoints} experience points!";
            battle.Comments.Add(comment);

            await storage.Update(battle);
        }

        private void LogAboutEvent(object @event)
        {
            Console.WriteLine();
            Console.WriteLine("===========================");
            Console.WriteLine($"Handling event {@event.GetType()} with payload {JsonConvert.SerializeObject(@event)}");
            Console.WriteLine("===========================");
            Console.WriteLine();
        }
    }
}