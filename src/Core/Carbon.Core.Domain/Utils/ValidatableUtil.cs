using Carbon.Core.Domain.Models.Interfaces;

namespace Carbon.Core.Domain.Utils;

/// <summary>
/// Утилита для валидации <see cref="IValidatable"/> объектов
/// </summary>
public static class ValidatableUtil
{
    /// <summary>
    /// Валидирует объекты <see cref="IValidatable"/>
    /// </summary>
    public static void ValidateAndThrow(params IValidatable[] items)
    {
        foreach (var item in items) item.ValidateAndThrow();
    }
}
