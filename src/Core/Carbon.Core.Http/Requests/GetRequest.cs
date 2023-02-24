using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Common.Interfaces;

/// <summary>
/// Базовый класс для GET запросов
/// </summary>
public abstract class GetRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected GetRequest(IRequestRoute? route = default, IRequestQuery? query = default) : base(route, query)
    {
    }
}
