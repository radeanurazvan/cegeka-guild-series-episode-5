using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class TrainerRegisteredEvent : IDomainEvent
    {
        private TrainerRegisteredEvent() { }

        internal TrainerRegisteredEvent(Trainer trainer)
            : this()
        {
            Id = trainer.Id;
            Name = trainer.Name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}