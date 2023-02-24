using Carbon.Contracts.Authentication.Responses;
using Carbon.Contracts.Routes.Server;
using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Requests;

namespace Carbon.Contracts.Authentication.PostRequests;

/// <summary>
/// Запрос регистрации пользователя
/// </summary>
public sealed class RegistrationRequest : PostRequest<AuthenticationResponse>
{
    public const string RouteConstant = $"{ServerRoutes.Controllers.AuthenticationController}/Registration";
    public override string RouteTemplate => RouteConstant;

    public RegistrationRequest(Body body) : base(body: body) { }

    /// <param name="Email">
    /// Email пользователя
    /// </param>
    /// <param name="Password">
    /// Пароль пользователя
    /// </param>
    public sealed record Body(string Username, string Email, string Password) : RequestBody;
}