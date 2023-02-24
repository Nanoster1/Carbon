namespace Carbon.Core.Domain.Exceptions;

public class EntityIsNotTransientException<TId> : Exception
{
    public EntityIsNotTransientException(string entityName, TId id)
        : base($"Entity is not transient. Entity: {entityName}. Id: {id}") { }
}