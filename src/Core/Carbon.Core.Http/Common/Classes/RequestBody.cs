using System.Net.Http.Json;

using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Common.Classes;

/// <summary>
/// Базовый класс тела запроса
/// </summary>
public abstract record RequestBody : IRequestBody
{
    public virtual JsonContent? GetBody() => JsonContent.Create((object)this);
}
