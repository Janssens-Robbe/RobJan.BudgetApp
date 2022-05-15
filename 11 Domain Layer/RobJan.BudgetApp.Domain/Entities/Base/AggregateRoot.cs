using RobJan.BudgetApp.Domain.Events.Base;

namespace RobJan.BudgetApp.Domain.Entities.Base;

public abstract class AggregateRoot<TAggregateRoot> : Entity<TAggregateRoot>
    where TAggregateRoot : AggregateRoot<TAggregateRoot>
{
    private readonly List<DomainEvent> _changes = new();

    protected AggregateRoot() { }

    protected AggregateRoot(IEnumerable<DomainEvent> changes)
    {
        _changes = changes.ToList();
    }

    private protected void Build()
    {
        foreach (var @event in _changes)
        {
            Handle(@event);
            EnsureValidation();
        }
    }

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
            throw new NotImplementedException($"{GetType().Name} does not have an implementation for event handler for {typeof(TEvent).Name}");

        eventHander.Handle(@event);
    }

    private void Handle(DomainEvent @event)
    {
        var handler = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
        if (!handler.IsInstanceOfType(this))
            throw new NotImplementedException($"{GetType().Name} does not have an implementation for event handler for {@event.GetType().Name}");

        handler.GetMethod("Handle")!.Invoke(this, new[] { @event });
    }

    protected abstract void EnsureValidation();
}
