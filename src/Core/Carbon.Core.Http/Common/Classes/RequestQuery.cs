using System.Web;

using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Common.Classes;

/// <summary>
/// Базовый класс QueryString запроса
/// </summary>
/// <remarks>
/// В своей основе использует рефлексию для получения значений свойств
/// </remarks>
public abstract record RequestQuery : IRequestQuery
{
    public virtual string GetQueryString()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(this);
            var stringValue = value?.ToString();
            if (string.IsNullOrWhiteSpace(stringValue)) continue;
            query[property.Name] = stringValue;
        }
        return query.ToString() ?? string.Empty;
    }
}
