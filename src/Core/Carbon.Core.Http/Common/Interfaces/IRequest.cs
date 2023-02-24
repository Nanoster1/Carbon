using System.Net.Http.Json;

namespace Carbon.Core.Http.Common.Interfaces;

/// <summary>
/// Интерфейс запроса
/// </summary>
/// <typeparam name="TResponse">
/// Тип ответа
/// </typeparam>
public interface IRequest<TResponse>
    where TResponse : notnull
{
    /// <summary>
    /// Маршрут запроса <br/>
    /// </summary>
    string GetRoute();
    /// <summary>
    /// Параметры в Query части запроса
    /// </summary>
    string GetQueryString();
    /// <summary>
    /// Тело запроса
    /// </summary>
    JsonContent? GetBodyContent();
}
