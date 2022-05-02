using RobJan.BudgetApp.Domain.Entities.Base;

namespace RobJan.BudgetApp.Domain.Entities.Contact;

public class ContactRoot : AggregateRoot<ContactRoot>
{
    #region State
    public string Name { get; set; }
    public ContactType Type { get; set; }
    #endregion State

    protected override void EnsureValidation() => throw new NotImplementedException();

    private protected override void Reset() => throw new NotImplementedException();
}
