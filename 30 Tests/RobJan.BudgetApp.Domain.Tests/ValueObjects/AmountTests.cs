using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using RobJan.BudgetApp.Common.Tests;
using RobJan.BudgetApp.Domain.Entities.ValueObjects;

namespace RobJan.BudgetApp.Domain.Tests.ValueObjects;

internal class AmountTests : TestBase
{
    [Test]
    public void From_CreatesAmount_WithValueAndCode()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);

        // Act
        var amount = Amount.From(value, currencyCode);

        // Assert
        amount.Value.Should().Be(value);
        amount.Currency.Code.Should().Be(currencyCode);
    }
}
