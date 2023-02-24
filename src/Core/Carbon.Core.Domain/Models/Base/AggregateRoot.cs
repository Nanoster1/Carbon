namespace Carbon.Core.Domain.Models.Base;

/// <summary>
/// Базовый класс для агрегата <br/>
/// Для создания объекта рекомендуется использовать фабричный метод Create <br/>
/// Все методы для взаимодействия с объектом должны: <br/> 
/// <list type="bullet">
/// <item> 
/// Вызывать у входящих параметров типа <see cref="ValueObject"/> метод <see cref="ValueObject.ValidateAndThrow"/>
/// </item>
/// <item>
/// Возвращать <see cref="ErrorOr.ErrorOr{TValue}"/>
/// </item>
/// </list>
/// Метод Create: <br/>
/// <list type="bullet">
/// <item>
/// Необходимо вызывать у всех ValueObject метод <see cref="ValueObject.ValidateAndThrow"/> <br/>
/// </item>
/// <item>
/// Возвращаемое значение должно быть <see cref="ErrorOr.ErrorOr{TValue}"/>
/// </item>
/// </list>
/// </summary>
/// <remarks>
/// <see cref="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-domain-model"/>
/// </remarks>
/// <inheritdoc/>
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : IEquatable<TId>
{
}