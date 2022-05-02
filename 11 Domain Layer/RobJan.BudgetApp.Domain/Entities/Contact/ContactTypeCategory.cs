using RobJan.BudgetApp.Domain.Entities.Base;

namespace RobJan.BudgetApp.Domain.Entities.Contact;

public class ContactTypeCategory : Entity<ContactTypeCategory>
{
    public string Name { get; internal set; }
}
