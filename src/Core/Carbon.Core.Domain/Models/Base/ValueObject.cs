using System;

using Carbon.Core.Domain.Models.Interfaces;

namespace Carbon.Core.Domain.Models.Base;

/// <summary>
/// Базовый класс, описывающий ValueObject <br/>
/// Для создания объекта рекомендуется использовать фабричный метод Create <br/>
/// Для валидации объекта нужно переопределить ВФ <see cref="OnValidateAndThrow"/>, которая должна выбрасывать исключения в случае ошибки <br/>
/// Может внутри содержать поля с примитивами
/// </summary>
/// <remarks>
/// <see cref="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects"/>
/// </remarks>
/// <value></value>
public abstract record ValueObject : IValidatable
{
    /// <summary>
    /// Показывает прошла ли проверка валидации
    /// </summary>
    public bool IsValid { get; private set; }

    public void ValidateAndThrow()
    {
        OnValidateAndThrow();
        IsValid = true;
    }

    /// <summary>
    /// ВФ для переопределения проверки валидации
    /// </summary>
    protected virtual void OnValidateAndThrow() { }
}