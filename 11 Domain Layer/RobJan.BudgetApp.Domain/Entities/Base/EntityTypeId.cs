using RobJan.BudgetApp.Common;
using RobJan.BudgetApp.Domain.Entities.Account;
using RobJan.BudgetApp.Domain.Entities.Contact;
using RobJan.BudgetApp.Domain.Entities.Transaction;

namespace RobJan.BudgetApp.Domain.Entities.Base;

internal static class EntityTypeId
{
    private static ushort GetTypeIdShort<T>() where T : Entity<T> => default(TokenOf<T>) switch
    {
        TokenOf<AccountRoot> => 1,
        TokenOf<ContactRoot> => 2,
        TokenOf<TransactionRoot> => 3,
        _ => throw new ArgumentException($"Type {typeof(T).Name} not assigned a type id")
    };

    public static byte[] GetTypeId<T>() where T : Entity<T>
    {
        var typeId = GetTypeIdShort<T>();

        return BitConverter.GetBytes(typeId);
    }
}
