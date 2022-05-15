using RobJan.BudgetApp.Domain.Entities.Account;
using RobJan.BudgetApp.Domain.Entities.Base;
using RobJan.BudgetApp.Domain.Events.Base;
using RobJan.BudgetApp.Domain.Events.Transaction;
using RobJan.BudgetApp.Domain.Exceptions;

namespace RobJan.BudgetApp.Domain.Entities.Transaction;

public class TransactionRoot : AggregateRoot<TransactionRoot>, IDomainEventHandler<TransactionRootCreated>
{
    private TransactionRoot() { }

    private TransactionRoot(IEnumerable<DomainEvent> changes)
        : base(changes)
    {
        Build();
    }

    #region State
    public TransactionAmount Amount { get; private set; } = TransactionAmount.Empty;
    public TransactionType Type { get; private set; } = TransactionType.Unknown;
    public DateTime TimeStamp { get; private set; }

    public AccountRoot? ReceivingAccount { get; private set; }
    public EntityId<AccountRoot>? ReceivingAccountId { get; private set; }

    public AccountRoot? SendingAccount { get; private set; }
    public EntityId<AccountRoot>? SendingAccountId { get; private set; }

    #endregion State

    public static TransactionRoot FromChanges(IEnumerable<DomainEvent> changes) => new(changes);

    #region Create

    public static TransactionRoot Create(
        decimal amount,
        string currency,
        TransactionType type,
        DateTime timeStamp,
        EntityId<AccountRoot>? receivingAccountId,
        EntityId<AccountRoot>? sendingAccountId)
    {
        var transaction = new TransactionRoot();

        transaction.Apply(new TransactionRootCreated(
            EntityId<TransactionRoot>.New(),
            amount,
            currency,
            type,
            timeStamp,
            receivingAccountId,
            sendingAccountId));

        return transaction;
    }

    void IDomainEventHandler<TransactionRootCreated>.Handle(TransactionRootCreated @event)
    {
        Id = @event.Id;
        Amount = TransactionAmount.From(@event.Amount, @event.Currency);
        Type = @event.Type;
        TimeStamp = @event.TimeStamp;
        ReceivingAccountId = @event.ReceivingAccountId;
        SendingAccountId = @event.SendingAccountId;
    }

    #endregion Create

    protected override void EnsureValidation()
    {
        if (TimeStamp == default)
            throw new InvalidStateException("TimeStamp is not set", nameof(TimeStamp));

        if (Id == EntityId<TransactionRoot>.Empty)
            throw new InvalidStateException("Id is not set", nameof(Id));

        _ = Type switch
        {
            TransactionType.Unknown => throw new InvalidStateException("Type is not set", nameof(Type)),
            TransactionType.Receive when ReceivingAccountId is null => throw new InvalidStateException("ReceivingAccountId is not set", nameof(ReceivingAccountId)),
            TransactionType.Send when SendingAccountId is null => throw new InvalidStateException("SendingAccountId is not set", nameof(SendingAccountId)),
            TransactionType.Transfer when ReceivingAccountId is null => throw new InvalidStateException("ReceivingAccountId is not set", nameof(ReceivingAccountId)),
            TransactionType.Transfer when SendingAccountId is null => throw new InvalidStateException("SendingAccountId is not set", nameof(SendingAccountId)),
            _ => true
        };
    }
}
