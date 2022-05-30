using RobJan.BudgetApp.Domain.Entities.Account;
using RobJan.BudgetApp.Domain.Entities.Base;
using RobJan.BudgetApp.Domain.Entities.Transaction;
using RobJan.BudgetApp.Domain.Events.Base;

namespace RobJan.BudgetApp.Domain.Events.Transaction;
public class TransactionRootCreated : DomainEvent, ICreationDomainEvent<TransactionRoot>
{
    public TransactionRootCreated(
        EntityId<TransactionRoot> id,
        decimal amount,
        string currency,
        TransactionType type,
        DateOnly date,
        EntityId<AccountRoot>? receivingAccountId,
        EntityId<AccountRoot>? sendingAccountId)
    {
        Id = id;
        Amount = amount;
        Currency = currency;
        Type = type;
        Date = date;
        ReceivingAccountId = receivingAccountId;
        SendingAccountId = sendingAccountId;
    }

    public EntityId<TransactionRoot> Id { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public TransactionType Type { get; init; }
    public DateOnly Date { get; init; }
    public EntityId<AccountRoot>? ReceivingAccountId { get; init; }
    public EntityId<AccountRoot>? SendingAccountId { get; init; }
}
