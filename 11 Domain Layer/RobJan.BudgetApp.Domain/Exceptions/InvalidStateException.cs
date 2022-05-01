namespace RobJan.BudgetApp.Domain.Exceptions;

public class InvalidStateException : Exception
{
    public InvalidStateException(string message, string member) : base(message)
    {
        Member = member;
    }

    public string Member { get; private init; }
}
