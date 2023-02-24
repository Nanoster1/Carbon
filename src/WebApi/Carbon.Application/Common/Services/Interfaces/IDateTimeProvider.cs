namespace Carbon.Application.Common.Services.Interfaces;

/// <summary>
/// Провайдер даты и времени
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Текущее время в UTC
    /// </summary>
    DateTimeOffset UtcNow { get; }
}