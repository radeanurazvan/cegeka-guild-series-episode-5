namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed partial class Battle
    {
        private void When(PlayerTookTurnEvent @event)
        {
            var victim = Attacker.Id == @event.PlayerId ? Defender : Attacker;
            victim.TakeDamage(@event.Ability.Damage);

            ActivePlayer = victim.Id;
        }

        private void When(PlayerAwardedEvent @event)
        {
            rewardedParticipants.Add(@event.PlayerId);
        }
    }
}