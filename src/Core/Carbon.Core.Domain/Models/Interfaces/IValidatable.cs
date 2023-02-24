namespace Carbon.Core.Domain.Models.Interfaces;

/// <summary>
/// Интерфейс, который показывает, что модель можно валидировать
/// </summary>
public interface IValidatable
{
    /// <summary>
    /// Валидирует модель <br/>
    /// Должен выбрасывать исключения в случае невалидных значений
    /// </summary>
    void ValidateAndThrow();
}