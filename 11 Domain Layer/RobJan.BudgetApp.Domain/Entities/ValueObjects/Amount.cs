using RobJan.BudgetApp.Common.Constants.Entities.ValueObjects;

namespace RobJan.BudgetApp.Domain.Entities.ValueObjects;

public class Amount : ValueObject<Amount>
{
    protected Amount(decimal value, Currency currency)
    {
        Value = value;
        Currency = currency;
    }

    public decimal Value { get; private init; }
    public Currency Currency { get; private init; }

    public static Amount From(decimal value, string currencyCode) => new(value, Currency.FromCode(currencyCode));

    public static Amount From(decimal value, Currency currency) => new(value, currency);

    public override string ToString() => $"{Value} {Currency}";

    #region Math
    public static Amount operator +(Amount a, Amount b)
    {
        return a.Currency != b.Currency
            ? throw new InvalidOperationException(AmountConstants.Exceptions.CanNotAddWithDifferentCurrencies)
            : new(a.Value + b.Value, a.Currency);
    }

    public static Amount operator -(Amount a, Amount b)
    {
        return a.Currency != b.Currency
            ? throw new InvalidOperationException(AmountConstants.Exceptions.CanNotSubtracWithDifferentCurrency)
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
    public override bool Equals(Amount? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value && Currency == other.Currency;
    }

    public override int GetHashCode() => HashCode.Combine(Value, Currency);
    #endregion Equality
}
