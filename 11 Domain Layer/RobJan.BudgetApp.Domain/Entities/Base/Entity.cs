namespace RobJan.BudgetApp.Domain.Entities.Base;

public abstract class Entity<TEntity>
    where TEntity : Entity<TEntity>
{
    public EntityId<TEntity> Id { get; private init; } = EntityId<TEntity>.New();
}
