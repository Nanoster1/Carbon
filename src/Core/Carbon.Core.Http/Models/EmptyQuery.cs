using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Models;

/// <summary>
/// Пустой Query запроса
/// </summary>
public sealed record EmptyQuery : IRequestQuery
{
    public string GetQueryString() => string.Empty;
}
