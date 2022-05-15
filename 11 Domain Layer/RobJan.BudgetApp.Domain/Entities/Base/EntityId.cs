namespace RobJan.BudgetApp.Domain.Entities.Base;

public struct EntityId<TEntity> : IEquatable<EntityId<TEntity>>
    where TEntity : Entity<TEntity>
{
    private readonly Guid _guid;
    private readonly byte[] _typeId;

    private EntityId(Guid guid)
    {
        _guid = guid;
        _typeId = EntityTypeId.GetTypeId<TEntity>();
    }

    private EntityId(Guid guid, byte[] typeId)
    {
        if (!typeId.SequenceEqual(EntityTypeId.GetTypeId<TEntity>()))
            throw new ArgumentException("Invalid entity type");

        _guid = guid;
        _typeId = typeId;
    }

    public Guid Guid => _guid;

    internal static EntityId<TEntity> Empty { get; } = new(Guid.Empty);

    public static bool operator ==(EntityId<TEntity> left, EntityId<TEntity> right) => left.Equals(right);
    public static bool operator !=(EntityId<TEntity> left, EntityId<TEntity> right) => !(left == right);

    internal static EntityId<TEntity> New() => new(Guid.NewGuid());
    internal static EntityId<TEntity> FromGuid(Guid guid) => new(guid);
    internal static EntityId<TEntity> FromHash(string hash)
    {
        var base64String = hash
            .Replace("-", "+")
            .Replace("_", "/");

        var bytes = Convert.FromBase64String(base64String);

        return new(new Guid(bytes[2..^0]), bytes[..2]);
    }

    public string GetHash()
    {
        var bytes = _typeId.Concat(_guid.ToByteArray()).ToArray();

        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_");
    }

    public override string ToString() => GetHash();

    public bool Equals(EntityId<TEntity> other) => _guid == other._guid && _typeId.SequenceEqual(other._typeId);
    public override bool Equals(object? obj) => obj is EntityId<TEntity> id && Equals(id);
    public override int GetHashCode() => HashCode.Combine(_guid, _typeId);
}
