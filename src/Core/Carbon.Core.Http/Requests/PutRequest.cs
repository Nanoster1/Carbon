using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Common.Interfaces;
using Carbon.Core.Http.Models;

namespace Carbon.Core.Http.Requests;

/// <summary>
/// Базовый класс для PUT запросов
/// </summary>
public abstract class PutRequest : Request<VoidResponse>
{
    protected PutRequest(IRequestRoute? route = null, IRequestQuery? query = null, IRequestBody? body = null)
        : base(route, query, body)
    {
    }
}
