using RobJan.BudgetApp.Common.Constants.Entities.TransactionAmount;
using RobJan.BudgetApp.Domain.Entities.ValueObjects;

namespace RobJan.BudgetApp.Domain.Entities.Transaction;

public class TransactionAmount : Amount
{
    protected TransactionAmount(decimal value, Currency currency)
        : base(value, currency)
    {
        if (value < 0) throw new ArgumentException(TransactionAmountConstants.Exceptions.ValueIsBelowZero, nameof(value));
    }

    public static new TransactionAmount From(decimal value, string currencyCode) => new(value, Currency.FromCode(currencyCode));

    public static new TransactionAmount From(decimal value, Currency currency) => new(value, currency);
}
