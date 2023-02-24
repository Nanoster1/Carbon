using System.Net.Http.Json;

using Carbon.Core.Http.Common.Interfaces;
using Carbon.Core.Http.Models;

namespace Carbon.Core.Http.Common.Classes;

/// <summary>
/// Базовый класс запроса
/// </summary>
/// <typeparam name="TResponse">
/// Тип ответа
/// </typeparam>
public abstract class Request<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
    protected Request(IRequestRoute? route = default, IRequestQuery? query = default, IRequestBody? body = default)
    {
        RouteParams = route ?? new EmptyRoute();
        QueryParams = query ?? new EmptyQuery();
        BodyContent = body ?? new EmptyBody();
    }

    /// <summary>
    /// Параметры маршрута
    /// </summary>
    public IRequestRoute RouteParams { get; set; }
    /// <summary>
    /// Параметры запроса
    /// </summary>
    public IRequestQuery QueryParams { get; set; }
    /// <summary>
    /// Тело запроса
    /// </summary>
    public IRequestBody BodyContent { get; set; }

    /// <summary>
    /// Шаблон маршрута запроса <br/>
    /// Пример: /api/users/{nameof(Route.Id)}
    /// </summary>
    public abstract string RouteTemplate { get; }

    public string GetRoute() => RouteParams.GetRouteString(RouteTemplate);
    public string GetQueryString() => QueryParams.GetQueryString();
    public JsonContent? GetBodyContent() => BodyContent.GetBody();
}
