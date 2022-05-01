using FluentAssertions;
using NUnit.Framework;
using RobJan.BudgetApp.Common.Constants.Entities.ValueObjects;
using RobJan.BudgetApp.Common.Tests;
using RobJan.BudgetApp.Domain.Entities.ValueObjects;
using System;

namespace RobJan.BudgetApp.Domain.Tests.ValueObjects;

internal class CurrencyTests : TestBase
{
    [Test]
    public void FromCode_CreatesCurrency()
    {
        // Arrange
        var code = Fixture.CreateString(3);

        // Act
        var currency = Currency.FromCode(code);

        // Assert
        currency.Code.Should().Be(code);
    }

    [Test]
    public void FromCode_ShouldThrowArgumentException_WhenCodeLongerThan3()
    {
        // Arrange
        var code = Fixture.CreateString(4);

        // Act
        var action = () => Currency.FromCode(code);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("code")
            .And.Message.Should().StartWith(CurrencyConstants.Exceptions.LengthNot3);
    }

    [Test]
    public void FromCode_ShouldThrowArgumentException_WhenCodeShorterThan3()
    {
        // Arrange
        var code = Fixture.CreateString(2);

        // Act
        var action = () => Currency.FromCode(code);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("code")
            .And.Message.Should().StartWith(CurrencyConstants.Exceptions.LengthNot3);
    }

    [Test]
    public void GetHashCode_ReturnsSameHashCode_WithSameCode()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code);
        var currency2 = Currency.FromCode(code);

        // Act
        var result1 = currency1.GetHashCode();
        var result2 = currency2.GetHashCode();

        // Assert
        result1.Should().Be(result2);
    }

    [Test]
    public void ToString_ReturnsCode()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency = Currency.FromCode(code);

        // Act
        var result = currency.ToString();

        // Assert
        result.Should().Be(code);
    }

    [Test]
    public void Equals_ReturnsTrue_WithSameCode()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code);
        var currency2 = Currency.FromCode(code);

        // Act
        var result = currency1.Equals(currency2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_ReturnsFalse_WithDifferentCode()
    {
        // Arrange
        var code1 = Fixture.CreateString(3);
        var code2 = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code1);
        var currency2 = Currency.FromCode(code2);

        // Act
        var result = currency1.Equals(currency2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsFalse_WithNull()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency = Currency.FromCode(code);

        // Act
        var result = currency.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsFalse_WithDifferentType()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency = Currency.FromCode(code);
        var code2 = Fixture.CreateString(3);

        // Act
        var result = currency.Equals(code2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsTrue_WithSameReference()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency = Currency.FromCode(code);

        // Act
        var result = currency.Equals(currency);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_ReturnsTrue_ForBoxedWithSameCode()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code);
        var currency2 = Currency.FromCode(code);

        // Act
        var result = currency1.Equals((object)currency2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_ReturnsFalse_ForBoxedWithDifferentCode()
    {
        // Arrange
        var code1 = Fixture.CreateString(3);
        var code2 = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code1);
        var currency2 = Currency.FromCode(code2);

        // Act
        var result = currency1.Equals((object)currency2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Equals_ReturnsFalse_ForBoxedWithNull()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency = Currency.FromCode(code);

        // Act
        var result = currency.Equals((object?)null);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void EqualsOperator_RetursTrue_WithSameCode()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code);
        var currency2 = Currency.FromCode(code);

        // Act
        var result = currency1 == currency2;

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void NotEqualsOperator_ReturnsTrue_WithDifferentCode()
    {
        // Arrange
        var code1 = Fixture.CreateString(3);
        var code2 = Fixture.CreateString(3);
        var currency1 = Currency.FromCode(code1);
        var currency2 = Currency.FromCode(code2);

        // Act
        var result = currency1 != currency2;

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void EqualsOperator_ReturnsFalse_WithNull()
    {
        // Arrange
        var code = Fixture.CreateString(3);
        var currency = Currency.FromCode(code);

        // Act
        var result = currency == null;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void EqualsOperator_ReturnsTrue_WithBothSidesNull()
    {
        // Arrange
        Currency? currency1 = null;
        Currency? currency2 = null;

        // Act
        var result = currency1 == currency2;

        // Assert
        result.Should().BeTrue();
    }
}
