namespace RobJan.BudgetApp.Domain.Entities.Base;

public abstract class Entity
{
    public Guid Id { get; private init; } = Guid.NewGuid();
}
