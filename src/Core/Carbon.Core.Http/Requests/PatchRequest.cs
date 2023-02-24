using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Requests;

/// <summary>
/// Базовый класс для запросов типа PATCH
/// </summary>
public abstract class PatchRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected PatchRequest(IRequestRoute? route = null, IRequestQuery? query = null, IRequestBody? body = null)
        : base(route, query, body)
    {
    }
}
