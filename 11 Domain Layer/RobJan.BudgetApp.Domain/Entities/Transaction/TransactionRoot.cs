using RobJan.BudgetApp.Domain.Entities.Account;
using RobJan.BudgetApp.Domain.Entities.Base;
using RobJan.BudgetApp.Domain.Exceptions;

namespace RobJan.BudgetApp.Domain.Entities.Transaction;

public class TransactionRoot : AggregateRoot
{
    #region State
    public TransactionAmount Amount { get; private set; }
    public TransactionDirection Direction { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public AccountRoot Account { get; private set; }
    public Guid AccountId { get; private set; }

    #endregion State

    protected override void EnsureValidation()
    {
        if (TimeStamp == default(DateTime))
            throw new InvalidStateException("TimeStamp is not set", nameof(TimeStamp));
    }

    private protected override void Reset() => throw new NotImplementedException();
}
