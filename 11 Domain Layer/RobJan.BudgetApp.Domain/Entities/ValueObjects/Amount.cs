namespace RobJan.BudgetApp.Domain.Entities.ValueObjects;

public class Amount : ValueObject, IEquatable<Amount>
{
    public Amount(decimal value, string currency)
    {
        if (currency.Length != 3) throw new ArgumentException("Currency must be 3 characters long", nameof(currency));

        Value = value;
        Currency = currency;
    }

    public decimal Value { get; private init; }
    public string Currency { get; private init; }

    #region Math
    public static Amount operator +(Amount a, Amount b)
    {
        return a.Currency != b.Currency
            ? throw new ArgumentException("Cannot add amounts with different currencies")
            : new(a.Value + b.Value, a.Currency);
    }

    public static Amount operator -(Amount a, Amount b)
    {
        return a.Currency != b.Currency
            ? throw new ArgumentException("Cannot subtract amounts with different currencies")
            : new(a.Value - b.Value, a.Currency);
    }

    public static Amount operator *(Amount a, int b) => new(a.Value * b, a.Currency);
    public static Amount operator *(Amount a, double b) => new(a.Value * (decimal)b, a.Currency);
    public static Amount operator *(Amount a, decimal b) => new(a.Value * b, a.Currency);
    public static Amount operator /(Amount a, int b) => new(a.Value / b, a.Currency);
    public static Amount operator /(Amount a, double b) => new(a.Value / (decimal)b, a.Currency);
    public static Amount operator /(Amount a, decimal b) => new(a.Value / b, a.Currency);
    #endregion Math

    #region Equality
    public static bool operator ==(Amount a, decimal b) => a.Value == b;
    public static bool operator !=(Amount a, decimal b) => a.Value != b;

    public bool Equals(Amount? other) => Equals(other);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not Amount amount)
            return false;

        return Value == amount.Value
            && Currency == amount.Currency;
    }

    public override int GetHashCode() => HashCode.Combine(Value, Currency);
    #endregion Equality
}
