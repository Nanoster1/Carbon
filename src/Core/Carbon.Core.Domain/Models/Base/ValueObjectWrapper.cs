using Carbon.Core.Domain.Exceptions;

namespace Carbon.Core.Domain.Models.Base;

/// <summary>
/// Базовый класс для ValueObject-обёртки над другим типом <br/>
/// Для создания объекта рекомендуется использовать фабричный метод Create <br/>
/// Для приведения к типу TValue можно использовать неявное приведение
/// </summary>
/// <typeparam name="TValue">Тип, который обёрнут в ValueObject</typeparam>
public abstract record ValueObjectWrapper<TValue> : ValueObject
    where TValue : notnull
{
    /// <summary>
    /// Значение, которое обёрнуто в ValueObject
    /// </summary>
    public TValue Value { get; init; } = default!;

    /// <summary>
    /// HashCode такой же, как и у <see cref="Value"/> <br/>
    /// Если <see cref="Value"/> равен <see langword="null"/>, то возвращается 0
    /// </summary>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    /// <summary>
    /// ToString такой же, как и у <see cref="Value"/>
    /// </summary>
    public override sealed string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }

    protected override void OnValidateAndThrow()
    {
        if (Value is null) throw new ValueObjectIsNotValidException(nameof(Value), "Value must be not null");
    }

    public static implicit operator TValue(ValueObjectWrapper<TValue> wrapper) => wrapper.Value;
}

/// <summary>
/// <inheritdoc/> <br/>
/// При реализации класса наследника необходимо использовать атрибут [Obsolete(ObsoleteMessage, true)] для пустого конструктора <br/>
/// </summary>
/// <typeparam name="TValue">
/// <inheritdoc/>
/// </typeparam>
/// <typeparam name="TSelf">
/// Конечный класс, который наследуется от ValueObjectWrapper
/// </typeparam>
public abstract record ValueObjectWrapper<TValue, TSelf> : ValueObjectWrapper<TValue>
    where TSelf : ValueObjectWrapper<TValue, TSelf>, new()
    where TValue : notnull
{
    /// <summary>
    /// Сообщение для пустого конструктора
    /// </summary>
    protected const string ObsoleteMessage = "Only for constrain new()";

    /// <summary>
    /// Пустой конструктор, необходимый для ограничения new() в TWrapper <br/>
    /// В классах наследниках необходимо использовать атрибут [Obsolete(ObsoleteMessage, true)]
    /// </summary>
    [Obsolete(ObsoleteMessage, true)]
    protected ValueObjectWrapper() { }

    protected ValueObjectWrapper(TValue value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        Value = value;
    }

    /// <summary>
    /// Фабричный метод <br/>
    /// Необходимо конструктор без параметров определять с атрибутом <see cref="ObsoleteAttribute"/> при наследовании <br/>
    /// Использовать именно этот метод для создания экземпляра, а конструкторы должны быть с модификатором <see langword="private"/> или <see langword="protected"/>
    /// </summary>
    public static TSelf Create(TValue value) => new() { Value = value };
}