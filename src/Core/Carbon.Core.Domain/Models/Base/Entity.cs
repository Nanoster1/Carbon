using Carbon.Core.Domain.Exceptions;

using ErrorOr;

namespace Carbon.Core.Domain.Models.Base;

/// <summary>
/// Базовый класс для сущности <br/>
/// Для создания объекта рекомендуется использовать фабричный метод Create <br/>
/// Метод Create: <br/>
/// Необходимо вызывать у всех ValueObject метод Validate <br/> 
/// Должен вызывать у всех ValueObject из параметров метод <see cref="ValueObject.ValidateAndThrow"/> <br/>
/// Конструкторы
/// </summary>
/// <remarks>
/// <see cref="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces"/>
/// </remarks>
/// <typeparam name="TId">Id сущности</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull, IEquatable<TId>
{
    /// <summary>
    /// Id сущности
    /// </summary>
    public TId Id { get; protected set; } = default!;
    /// <summary>
    /// Показывает есть ли у сущности Id
    /// </summary>
    public bool IsTransient => Id.Equals(default);
    /// <summary>
    /// Установить Id сущности <br/>
    /// Если Id уже установлен, то будет выброшено исключение <see cref="EntityIsNotTransientException{TId}"/>
    /// </summary>
    /// <exception cref="EntityIsNotTransientException{TId}">Если Id уже установлен</exception>
    public virtual ErrorOr<Updated> SetId(TId id)
    {
        if (IsTransient)
        {
            Id = id;
            return Result.Updated;
        }

        throw new EntityIsNotTransientException<TId>(GetType().Name, Id);
    }
    /// <summary>
    /// Сравнение сущностей осуществляется по Id
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Entity<TId> entity) return false;
        return Id.Equals(entity.Id);
    }
    /// <summary>
    /// Сравнение сущностей осуществляется по Id
    /// </summary>
    public bool Equals(Entity<TId>? other) => Equals((object?)other);
    /// <summary>
    /// HashCode такой же, как и у <typeparamref name="TId"/>
    /// </summary>
    public override int GetHashCode() => Id.GetHashCode();

    #region Operators

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }

    #endregion
}