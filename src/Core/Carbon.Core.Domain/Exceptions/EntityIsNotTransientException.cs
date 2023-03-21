namespace Carbon.Core.Domain.Exceptions;

/// <summary>
/// Исключение, означающее, что сущность сохранена в базе данных
/// </summary>
public class EntityIsNotTransientException<TId> : Exception
{
    public EntityIsNotTransientException(string entityName, TId id)
        : base($"Entity is not transient. Entity: {entityName}. Id: {id}") { }
}
