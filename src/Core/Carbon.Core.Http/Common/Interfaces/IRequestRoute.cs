namespace Carbon.Core.Http.Common.Interfaces;

/// <summary>
/// Интерфейс маршрута запроса
/// </summary>
public interface IRequestRoute
{
    /// <summary>
    /// Маршрут запроса в виде единой строки
    /// </summary>
    /// <param name="routeTemplate">
    /// Шаблон маршрута <br/>
    /// Например: "api/users/{nameof(Route.Id)}"
    /// </param>
    string GetRouteString(string routeTemplate);
}
