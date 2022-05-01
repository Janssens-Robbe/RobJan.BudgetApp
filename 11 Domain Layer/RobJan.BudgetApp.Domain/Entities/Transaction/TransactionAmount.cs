using RobJan.BudgetApp.Domain.Entities.ValueObjects;

namespace RobJan.BudgetApp.Domain.Entities.Transaction;

public class TransactionAmount : Amount
{
    public TransactionAmount(decimal value, string currency)
        : base(value, currency)
    {
        if (value < 0) throw new ArgumentException("Transaction amount cannot be negative", nameof(value));
    }
}
