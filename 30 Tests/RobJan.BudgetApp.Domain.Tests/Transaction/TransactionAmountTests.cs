using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using RobJan.BudgetApp.Common.Constants.Entities.TransactionAmount;
using RobJan.BudgetApp.Common.Tests;
using RobJan.BudgetApp.Domain.Entities.Transaction;
using RobJan.BudgetApp.Domain.Entities.ValueObjects;
using System;

namespace RobJan.BudgetApp.Domain.Tests.Transaction;
internal class TransactionAmountTests : TestBase
{
    [Test]
    public void From_CreatesTransactionAmount_WithValueAndCurrencyCode()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);

        // Act
        var result = TransactionAmount.From(value, currencyCode);

        // Assert
        result.Value.Should().Be(value);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void From_CreatesTransactionAmount_WithValueAndCurrency()
    {

        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var currency = Currency.FromCode(currencyCode);

        // Act
        var result = TransactionAmount.From(value, currency);

        // Assert
        result.Value.Should().Be(value);
        result.Currency.Should().Be(currency);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void From_ThrowsArgumentException_WhenValusIsBelowZero()
    {
        // Arrange
        var value = -Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);

        // Act
        var action = () => TransactionAmount.From(value, currencyCode);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("value")
            .And.Message.Should().StartWith(TransactionAmountConstants.Exceptions.ValueIsBelowZero);
    }
}
