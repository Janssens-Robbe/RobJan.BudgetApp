using AutoFixture;
using System;

namespace RobJan.BudgetApp.Common.Tests;

public abstract class TestBase
{
    public TestBase()
    {
        Fixture.Register<DateTime, DateOnly>(dateTime => DateOnly.FromDateTime(dateTime));
        Fixture.Register<DateTime, TimeOnly>(dateTime => TimeOnly.FromDateTime(dateTime));
    }

    protected Fixture Fixture { get; } = new Fixture();
}
