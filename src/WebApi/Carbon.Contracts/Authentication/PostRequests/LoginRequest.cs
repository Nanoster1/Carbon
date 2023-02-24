using Carbon.Contracts.Authentication.Responses;
using Carbon.Contracts.Common.Routes.Server;
using Carbon.Core.Http.Common.Classes;
using Carbon.Core.Http.Requests;

namespace Carbon.Contracts.Authentication.PostRequests;

/// <summary>
/// Запрос входа пользователя
/// </summary>
public sealed class LoginRequest : PostRequest<AuthenticationResponse>
{
    public const string RouteConstant = $"{ServerRoutes.Controllers.AuthenticationController}/Login";
    public override string RouteTemplate => RouteConstant;

    public LoginRequest(Body body) : base(body: body) { }

    /// <param name="Email">
    /// Email пользователя
    /// </param>
    /// <param name="Password">
    /// Пароль пользователя
    /// </param>
    /// <returns></returns>
    public sealed record Body(string Email, string Password) : RequestBody;
}