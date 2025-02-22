﻿using System;
using System.Collections.Generic;
using System.Reflection;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Common
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> events = new List<IDomainEvent>();

        public Guid GetId() => this.Id;

        public IReadOnlyList<IDomainEvent> Events => events;

        protected Result AddDomainEvent(IDomainEvent @event)
        {
            return Maybe<IDomainEvent>.From(@event)
                .ToResult("Event cannot be null")
                .Tap(e => events.Add(e));
        }

        protected Result ReactToDomainEvent(IDomainEvent @event)
        {
            return AddDomainEvent(@event)
                .Tap(() => Mutate(@event));
        }

        public void ClearEvents()
        {
            events.Clear();
        }

        public async void Mutate<T>(T @event)
            where T : IDomainEvent
        {
            await Maybe<T>.From(@event)
                .ToResult("Cannot mutate null event")
                .Map(_ => GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                .Map(methods => methods.FirstOrNothing(m => m.Name == nameof(When) && m.GetParameters()[0].ParameterType == @event.GetType()))
                .Bind(whenMethodOrNothing => whenMethodOrNothing.ToResult($"When method not found for {@event.GetType().Name}"))
                .OnFailure(e => throw new InvalidOperationException(e))
                .Tap(m => m.Invoke(this, new object[] { @event }));
        }

        private void When()
        {
        }
    }
}