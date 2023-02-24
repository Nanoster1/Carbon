using Carbon.Application.Common.Services.Interfaces;

namespace Carbon.Infrastructure.Services;

/// <summary>
/// Реализация провайдера даты и времени
/// </summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}