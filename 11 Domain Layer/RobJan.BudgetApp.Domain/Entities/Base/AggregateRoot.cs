using RobJan.BudgetApp.Domain.Events.Base;

namespace RobJan.BudgetApp.Domain.Entities.Base;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _changes = new();

    protected AggregateRoot() { }

    protected AggregateRoot(IEnumerable<DomainEvent> changes)
    {
        _changes = new(changes);
        Rebuild();
    }

    private protected void Rebuild()
    {
        Reset();
        foreach (var @event in _changes)
            Handle(@event);
        EnsureValidation();
    }

    private protected abstract void Reset();

    protected void Apply<TEvent>(TEvent @event)
        where TEvent : DomainEvent
    {
        Handle(@event);
        EnsureValidation();
        _changes.Add(@event);
    }

    private void Handle<TEvent>(TEvent @event)
        where TEvent : DomainEvent
    {
        if (this is not IDomainEventHandler<TEvent> eventHander)
            throw new NotImplementedException($"{GetType().Name} does not have an implementation for event handler {typeof(IDomainEventHandler<TEvent>).Name}");

        eventHander.Handle(@event);
    }

    protected abstract void EnsureValidation();
}
