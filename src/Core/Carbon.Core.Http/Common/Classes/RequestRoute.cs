using System.Text;

using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Common.Classes;

/// <summary>
/// Базовый класс маршрута запроса <br/>
/// Подставляет значения свойств в шаблон
/// </summary>
/// <remarks>
/// В своей основе использует рефлексию для получения значений свойств
/// </remarks>
public abstract record RequestRoute : IRequestRoute
{
    public virtual string GetRouteString(string routeTemplate)
    {
        var route = new StringBuilder(routeTemplate);
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(this)?.ToString();
            if (value is not null)
            {
                route = route.Replace($"{{{property.Name}}}", value);
            }
        }
        return route.ToString();
    }
}
