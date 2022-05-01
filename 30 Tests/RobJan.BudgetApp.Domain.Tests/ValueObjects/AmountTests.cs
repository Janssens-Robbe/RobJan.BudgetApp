using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using RobJan.BudgetApp.Common.Constants.Entities.ValueObjects;
using RobJan.BudgetApp.Common.Tests;
using RobJan.BudgetApp.Domain.Entities.ValueObjects;
using System;

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

    [Test]
    public void From_CreatesAmount_WithValueAndCurrency()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currency = Currency.FromCode(Fixture.CreateString(3));

        // Act
        var amount = Amount.From(value, currency);

        // Assert
        amount.Value.Should().Be(value);
        amount.Currency.Should().Be(currency);
    }

    [Test]
    public void ToString_ReturnsValueAndCurrencyCode()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);

        // Act
        var result = amount.ToString();

        // Assert
        result.Should().Be($"{value} {currencyCode}");
    }

    [Test]
    public void Equals_ReturnsTrue_WhenValueIsEqual()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount1 = Amount.From(value, currencyCode);
        var amount2 = Amount.From(value, currencyCode);

        // Act
        var result = amount1.Equals(amount2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_ReturnsFalse_WhenValueIsNotEqual()
    {
        // Arrange
        var value1 = Fixture.Create<decimal>();
        var value2 = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount1 = Amount.From(value1, currencyCode);
        var amount2 = Amount.From(value2, currencyCode);

        // Act
        var result = amount1.Equals(amount2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsFalse_WhenCurrencyIsNotEqual()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode1 = Fixture.CreateString(3);
        var currencyCode2 = Fixture.CreateString(3);
        var amount1 = Amount.From(value, currencyCode1);
        var amount2 = Amount.From(value, currencyCode2);

        // Act
        var result = amount1.Equals(amount2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsFalse_WhenAmountIsNull()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);

        // Act
        var result = amount.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsTrue_WithSameReference()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);

        // Act
        var result = amount.Equals(amount);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void GetHashCode_ReturnsSameHashCode_WhenValueAndCurrencyAreEqual()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount1 = Amount.From(value, currencyCode);
        var amount2 = Amount.From(value, currencyCode);

        // Act
        var hashCode1 = amount1.GetHashCode();
        var hashCode2 = amount2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Test]
    public void PlusOperator_ReturnsSum()
    {
        // Arrange
        var value1 = Fixture.Create<decimal>();
        var value2 = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount1 = Amount.From(value1, currencyCode);
        var amount2 = Amount.From(value2, currencyCode);

        // Act
        var result = amount1 + amount2;

        // Assert
        result.Value.Should().Be(value1 + value2);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void MinusOperator_ReturnsDifference()
    {
        // Arrange
        var value1 = Fixture.Create<decimal>();
        var value2 = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount1 = Amount.From(value1, currencyCode);
        var amount2 = Amount.From(value2, currencyCode);

        // Act
        var result = amount1 - amount2;

        // Assert
        result.Value.Should().Be(value1 - value2);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void PlusOperator_ThrowsException_WhenCurrencyIsNotEqual()
    {
        // Arrange
        var value1 = Fixture.Create<decimal>();
        var value2 = Fixture.Create<decimal>();
        var currencyCode1 = Fixture.CreateString(3);
        var currencyCode2 = Fixture.CreateString(3);
        var amount1 = Amount.From(value1, currencyCode1);
        var amount2 = Amount.From(value2, currencyCode2);

        // Act
        var action = () => amount1 + amount2;

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage(AmountConstants.Exceptions.CanNotAddWithDifferentCurrencies);
    }

    [Test]
    public void MinusOperator_ThrowsException_WhenCurrencyIsNotEqual()
    {
        // Arrange
        var value1 = Fixture.Create<decimal>();
        var value2 = Fixture.Create<decimal>();
        var currencyCode1 = Fixture.CreateString(3);
        var currencyCode2 = Fixture.CreateString(3);
        var amount1 = Amount.From(value1, currencyCode1);
        var amount2 = Amount.From(value2, currencyCode2);

        // Act
        var action = () => amount1 - amount2;

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage(AmountConstants.Exceptions.CanNotSubtracWithDifferentCurrency);
    }

    [Test]
    public void MultiplyOperator_ReturnsProduct_WithInt()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);
        var multiplier = Fixture.Create<int>();

        // Act
        var result = amount * multiplier;

        // Assert
        result.Value.Should().Be(value * multiplier);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void MultiplyOperator_RetrunsProduct_WithDobule()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);
        var multiplier = Fixture.Create<double>();

        // Act
        var result = amount * multiplier;

        // Assert
        result.Value.Should().Be(value * (decimal)multiplier);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void MultiplyOperator_ReturnsProduct_WithDecimal()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);
        var multiplier = Fixture.Create<decimal>();

        // Act
        var result = amount * multiplier;

        // Assert
        result.Value.Should().Be(value * multiplier);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void DevideOperator_ReturnsQuotient_WithInt()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);
        var divisor = Fixture.Create<int>();

        // Act
        var result = amount / divisor;

        // Assert
        result.Value.Should().Be(value / divisor);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void DevideOperator_ReturnsQuotient_WithDouble()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);
        var divisor = Fixture.Create<double>();

        // Act
        var result = amount / divisor;

        // Assert
        result.Value.Should().Be(value / (decimal)divisor);
        result.Currency.Code.Should().Be(currencyCode);
    }

    [Test]
    public void DevideOperator_ReturnsQuotient_WithDecimal()
    {
        // Arrange
        var value = Fixture.Create<decimal>();
        var currencyCode = Fixture.CreateString(3);
        var amount = Amount.From(value, currencyCode);
        var divisor = Fixture.Create<decimal>();

        // Act
        var result = amount / divisor;

        // Assert
        result.Value.Should().Be(value / divisor);
        result.Currency.Code.Should().Be(currencyCode);
    }
}
