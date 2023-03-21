using Carbon.Application.Authentication.Commands.Registration;
using Carbon.Application.Authentication.Queries.Login;
using Carbon.Application.Authentication.Results;
using Carbon.Contracts.Authentication.PostRequests;
using Carbon.Contracts.Authentication.Responses;
using Carbon.Core.Mapster.Extensions;
using Carbon.Server.Controllers;

using Mapster;

namespace Carbon.Server.Mapping.Configurations;

/// <summary>
/// Конфиг для <see cref="AuthenticationController"/>
/// </summary>
public sealed class AuthenticationMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.MapRequests<LoginRequest.Body, LoginQuery>();
        config.MapRequests<RegistrationRequest.Body, RegistrationCommand>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>();
    }
}
