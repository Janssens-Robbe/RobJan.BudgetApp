namespace RobJan.BudgetApp.Domain.Entities.ValueObjects;

public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
{
    public override abstract string ToString();

    public static bool operator ==(ValueObject<TValueObject>? left, ValueObject<TValueObject>? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(ValueObject<TValueObject>? left, ValueObject<TValueObject>? right) => !(left == right);

    public abstract bool Equals(TValueObject? other);

    public sealed override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not TValueObject valueObject) return false;
        return Equals(valueObject);
    }

    public override abstract int GetHashCode();
}
