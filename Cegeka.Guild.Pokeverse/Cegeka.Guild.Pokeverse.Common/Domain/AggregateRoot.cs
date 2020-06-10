using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Common
{
    public interface IAggregateRoot
    {
        string GetId();

        IReadOnlyList<IDomainEvent> Events { get; }

        void ClearEvents();
    }

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly Dictionary<Type, MethodInfo> whenMethods;
        private readonly List<IDomainEvent> events = new List<IDomainEvent>();

        protected AggregateRoot()
        {
            this.whenMethods = GetPartitionedWhenMethods();
        }

        public string GetId() => this.Id.ToString();

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

        public void Mutate<T>(T @event)
            where T : IDomainEvent
        {
            if (@event == null)
            {
                throw new InvalidOperationException("Cannot mutate null event");
            }

            var hasWhenMethod = whenMethods.TryGetValue(@event.GetType(), out var whenMethod);
            if (!hasWhenMethod)
            {
                throw new InvalidOperationException($"No When method defined for {@event.GetType().Name}");
            }

            whenMethod.Invoke(this, new object[] { @event });
        }

        private void When()
        {
        }

        private Dictionary<Type, MethodInfo> GetPartitionedWhenMethods()
        {
            return GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.Name == nameof(When) && m.GetParameters().Any())
                .ToDictionary(m => m.GetParameters()[0].ParameterType, m => m);
        }
    }
}