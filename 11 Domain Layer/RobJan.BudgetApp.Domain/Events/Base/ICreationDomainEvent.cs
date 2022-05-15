using RobJan.BudgetApp.Domain.Entities.Base;

namespace RobJan.BudgetApp.Domain.Events.Base;

public interface ICreationDomainEvent<TAggregate>
    where TAggregate : AggregateRoot<TAggregate>
{
    EntityId<TAggregate> Id { get; }
}
