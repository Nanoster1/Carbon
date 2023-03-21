using Carbon.Core.Domain.Models.Interfaces;

namespace Carbon.Core.Domain.Utils;

/// <summary>
/// Утилита для получения стандартного значения
/// </summary>
public static class DefaultValueUtil
{
    /// <summary>
    /// Получение стандартного значения для объекта
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    /// <returns>Стандартное значение для объекта</returns>
    public static T MakeDefault<T>() where T : IHasDefaultValue<T>
    {
        return T.Default;
    }
}
