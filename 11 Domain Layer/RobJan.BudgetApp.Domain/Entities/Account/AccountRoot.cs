using RobJan.BudgetApp.Domain.Entities.Base;

namespace RobJan.BudgetApp.Domain.Entities.Account;
public class AccountRoot : AggregateRoot<AccountRoot>
{
    protected override void EnsureValidation() => throw new NotImplementedException();
}
