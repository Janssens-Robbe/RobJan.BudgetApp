using RobJan.BudgetApp.Domain.Events.Base;

namespace RobJan.BudgetApp.Domain.Entities.Base;

internal interface IDomainEventHandler<TEvent>
    where TEvent : DomainEvent
{
    void Handle(TEvent @event);
}
