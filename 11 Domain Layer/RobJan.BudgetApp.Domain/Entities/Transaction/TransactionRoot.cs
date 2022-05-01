using RobJan.BudgetApp.Domain.Entities.Base;

namespace RobJan.BudgetApp.Domain.Entities.Transaction;

public class TransactionRoot : AggregateRoot
{
    #region State
    public TransactionAmount Amount { get; private set; }
    public TransactionDirection Direction { get; private set; }

    #endregion State

    protected override void EnsureValidation() => throw new NotImplementedException();

    private protected override void Reset() => throw new NotImplementedException();
}
