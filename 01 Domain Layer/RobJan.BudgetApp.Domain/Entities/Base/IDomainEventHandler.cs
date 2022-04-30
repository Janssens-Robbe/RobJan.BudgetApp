using RobJan.BudgetApp.Domain.Events.Base;

namespace RobJan.BudgetApp.Domain.Entities.Base
{
    internal interface IDomainEventHandler<T>
        where T : DomainEvent
    {
        internal void Handle(T @event);
    }
}
