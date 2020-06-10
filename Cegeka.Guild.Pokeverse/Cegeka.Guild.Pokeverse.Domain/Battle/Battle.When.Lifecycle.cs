namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed partial class Battle
    {
        private void When(BattleStartedEvent @event)
        {
            Id = @event.Id;
            ActivePlayer = @event.Attacker.Id;
            Attacker = new PokemonInFight(@event.Attacker.Id, @event.Attacker.Name, @event.Attacker.TrainerId, @event.Attacker.Health);
            Defender = new PokemonInFight(@event.Defender.Id, @event.Defender.Name, @event.Defender.TrainerId, @event.Defender.Health);
            StartedAt = @event.StartedAt;
        }

        private void When(BattleEndedEvent @event)
        {
            FinishedAt = @event.EndedAt;
        }
    }
}