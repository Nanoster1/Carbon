using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Requests;

/// <summary>
/// Базовый класс для запросов типа POST
/// </summary>
public abstract class PostRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected PostRequest(IRequestRoute? route = default, IRequestQuery? query = null, IRequestBody? body = null)
        : base(route, query, body)
    {
    }
}
