using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using RobJan.BudgetApp.Common.Tests;
using RobJan.BudgetApp.Domain.Entities.Account;
using RobJan.BudgetApp.Domain.Entities.Base;
using RobJan.BudgetApp.Domain.Entities.Transaction;
using RobJan.BudgetApp.Domain.Events.Base;
using RobJan.BudgetApp.Domain.Events.Transaction;
using System;
using System.Collections.Generic;

namespace RobJan.BudgetApp.Domain.Tests.Transaction;
internal class TransactionRootTests : TestBase
{
    [Test]
    public void Create_FillsInAllPropertiesAndCreatesId()
    {
        // Arrange
        var amount = Fixture.Create<decimal>();
        var currency = Fixture.CreateString(3);
        var type = TransactionType.Receive;
        var timeStamp = Fixture.Create<DateTime>();
        var receivingAccountId = EntityId<AccountRoot>.New();

        // Act
        var result = TransactionRoot.Create(amount, currency, type, timeStamp, receivingAccountId, null);

        // Assert
        result.Id.Should().NotBe(EntityId<TransactionRoot>.Empty);
        result.Amount.Value.Should().Be(amount);
        result.Amount.Currency.Code.Should().Be(currency);
        result.Type.Should().Be(type);
        result.TimeStamp.Should().Be(timeStamp);
        result.ReceivingAccountId.Should().Be(receivingAccountId);
        result.SendingAccountId.Should().Be(null);
    }

    [Test]
    public void FromChanges_CreatesTransaction()
    {
        // Arrange
        var id = EntityId<TransactionRoot>.New();
        var amount = Fixture.Create<decimal>();
        var currency = Fixture.CreateString(3);
        var type = TransactionType.Receive;
        var timeStamp = Fixture.Create<DateTime>();
        var receivingAccountId = EntityId<AccountRoot>.New();

        var @event = new TransactionRootCreated(id, amount, currency, type, timeStamp, receivingAccountId, null);
        var changes = new List<DomainEvent>() { @event };

        // Act
        var result = TransactionRoot.FromChanges(changes);

        // Assert
        result.Id.Should().Be(id);
        result.Amount.Value.Should().Be(amount);
        result.Amount.Currency.Code.Should().Be(currency);
        result.Type.Should().Be(type);
        result.TimeStamp.Should().Be(timeStamp);
        result.ReceivingAccountId.Should().Be(receivingAccountId);
        result.SendingAccountId.Should().Be(null);
    }
}
