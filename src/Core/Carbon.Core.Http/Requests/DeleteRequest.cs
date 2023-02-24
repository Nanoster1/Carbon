using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Common.Interfaces;

namespace Carbon.Core.Http.Requests;

/// <summary>
/// Базовый класс для запросов типа DELETE
/// </summary>
public abstract class DeleteRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected DeleteRequest(IRequestRoute? route = null, IRequestQuery? query = null) : base(route, query)
    {
    }
}
