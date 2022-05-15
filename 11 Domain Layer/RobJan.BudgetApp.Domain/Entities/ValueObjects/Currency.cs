using RobJan.BudgetApp.Common.Constants.Entities.ValueObjects;

namespace RobJan.BudgetApp.Domain.Entities.ValueObjects;

public class Currency : ValueObject<Currency>, IEquatable<string>
{
    private Currency(string code)
    {
        if (code.Length != 3) throw new ArgumentException(CurrencyConstants.Exceptions.LengthNot3, nameof(code));

        Code = code;
    }

    public string Code { get; private init; }

    public static Currency Unknown { get; } = new Currency("???");

    public static Currency FromCode(string code) => new(code);

    public static implicit operator Currency(string code) => new(code);

    public override string ToString() => Code;

    #region Equality
    public override bool Equals(Currency? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Code == other.Code;
    }

    public override int GetHashCode() => Code.GetHashCode();

    public bool Equals(string? other) => Code == other;

    #endregion Equality
}
