using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using RobJan.BudgetApp.Common.Tests;
using RobJan.BudgetApp.Domain.Entities.Base;
using RobJan.BudgetApp.Domain.Entities.Contact;
using RobJan.BudgetApp.Domain.Entities.Transaction;
using System;
using System.Linq;

namespace RobJan.BudgetApp.Domain.Tests.Base;

internal class EntityIdTests : TestBase
{
    [Test]
    public void GetHash_ReturnsCorrectHash()
    {
        // Arrange
        var guid = Fixture.Create<Guid>();
        var entityId = EntityId<TransactionRoot>.FromGuid(guid);
        var typeId = BitConverter.GetBytes((ushort)3);
        var expectedHash = Convert.ToBase64String(typeId.Concat(guid.ToByteArray()).ToArray())
            .Replace("+", "-")
            .Replace("/", "_");

        // Act
        var result = entityId.GetHash();

        // Assert
        result.Should().NotEndWith("=");
        result.Should().NotContain("+");
        result.Should().NotContain("/");
        result.Should().Be(expectedHash);
    }

    [Test]
    public void New_CreatesNewId()
    {
        // Act
        var result = EntityId<TransactionRoot>.New();

        // Assert
        result.Guid.Should().NotBeEmpty();
    }

    [Test]
    public void FromHash_CreatesEntityId_WithCorrectGuid()
    {
        // Arrange
        var guid = Fixture.Create<Guid>();
        var entityId = EntityId<TransactionRoot>.FromGuid(guid);

        // Act
        var result = EntityId<TransactionRoot>.FromHash(entityId.GetHash());

        // Assert
        result.Guid.Should().Be(guid);
    }

    [Test]
    public void FromHash_ThrowsArgumentException_WhenEntityTypeIsWrong()
    {
        // Arrange
        var entityId = EntityId<TransactionRoot>.FromGuid(Fixture.Create<Guid>());

        // Act
        var action = () => EntityId<ContactRoot>.FromHash(entityId.GetHash());

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Invalid entity type");
    }
}
