namespace RobJan.BudgetApp.Domain.Entities.ValueObjects;

public abstract class ValueObject
{
    public abstract override bool Equals(object? obj);
    public abstract override int GetHashCode();
}
