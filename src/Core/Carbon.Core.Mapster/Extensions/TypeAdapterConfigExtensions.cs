using Mapster;

namespace Carbon.Core.Mapster.Extensions;

public static class TypeAdapterConfigExtensions
{
    /// <summary>
    /// Маппит Web - запросы на Application - запросы
    /// </summary>
    /// <typeparam name="TWebRequest">
    /// Тип Web - запроса
    /// </typeparam>
    /// <typeparam name="TApplicationRequest">
    /// Тип Application - запроса
    /// </typeparam>
    public static TypeAdapterSetter<TWebRequest, TApplicationRequest> MapRequests<TWebRequest, TApplicationRequest>(this TypeAdapterConfig config)
    {
        return config.NewConfig<TWebRequest, TApplicationRequest>()
            .MapToConstructor(true)
            .RequireDestinationMemberSource(true)
            .IgnoreNullValues(false);
    }
}
