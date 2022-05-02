using RobJan.BudgetApp.Domain.Entities.Base;

namespace RobJan.BudgetApp.Domain.Entities.Contact;

public class ContactType : Entity<ContactType>
{
    public string Name { get; internal set; }
    public ContactTypeCategory Category { get; internal set; }
}
