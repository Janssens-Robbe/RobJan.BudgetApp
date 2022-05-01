using AutoFixture;
using System.Linq;
using System.Text;

namespace RobJan.BudgetApp.Common.Tests;
public static class AutoFixtureExtensions
{
    public static string CreateString(this IFixture fixture, int length)
    {
        var builder = new StringBuilder();
        foreach (int _ in Enumerable.Range(0, length))
            builder.Append(fixture.Create<char>());
        return builder.ToString();
    }
}
