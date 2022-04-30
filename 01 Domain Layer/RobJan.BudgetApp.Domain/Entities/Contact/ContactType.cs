namespace RobJan.BudgetApp.Domain.Entities.Contact;

public class ContactType : Entity
{
    public string Name { get; internal set; }
    public ContactTypeCategory Category { get; internal set; }
}
