using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Models;

/// <summary>
/// Пустой маршрут запроса
/// </summary>
public sealed record EmptyRoute : IRequestRoute
{
    public string GetRouteString(string routeTemplate) => routeTemplate;
}
