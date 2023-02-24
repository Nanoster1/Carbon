using System.Net.Http.Json;

using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Models;

/// <summary>
/// Пустое тело запроса
/// </summary>
public sealed record EmptyBody : IRequestBody
{
    public JsonContent? GetBody() => null;
}
