using AutoFixture;

namespace RobJan.BudgetApp.Common.Tests;

public abstract class TestBase
{
    protected Fixture Fixture { get; } = new Fixture();
}
