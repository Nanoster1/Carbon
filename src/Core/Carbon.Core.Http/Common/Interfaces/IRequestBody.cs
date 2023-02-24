using System.Net.Http.Json;

namespace Carbon.Core.Http.Common.Interfaces;

/// <summary>
/// Интерфейс тела запроса
/// </summary>
public interface IRequestBody
{
    /// <summary>
    /// Тело запроса в виде <see cref="JsonContent"/>
    /// </summary>
    JsonContent? GetBody();
}
