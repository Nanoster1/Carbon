namespace Carbon.Core.Http.Common.Interfaces;

/// <summary>
/// Интерфейс QueryString запроса
/// </summary>
public interface IRequestQuery
{
    /// <summary>
    /// Query запроса в виде строки
    /// </summary>
    string GetQueryString();
}
