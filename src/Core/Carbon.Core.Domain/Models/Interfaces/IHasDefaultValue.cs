namespace Carbon.Core.Domain.Models.Interfaces;

/// <summary>
/// Интерфейс для получения стандартного значения для объекта
/// </summary>
public interface IHasDefaultValue<TSelf>
    where TSelf : IHasDefaultValue<TSelf>
{
    /// <summary>
    /// Стандартное значение для объекта
    /// </summary>
    public static abstract TSelf Default { get; }
}
