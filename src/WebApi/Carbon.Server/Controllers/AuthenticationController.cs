using Carbon.Application.Authentication.Commands;
using Carbon.Application.Authentication.Queries;
using Carbon.Contracts.Authentication.PostRequests;
using Carbon.Contracts.Authentication.Responses;
using Carbon.Contracts.Common.Routes.Server;
using Carbon.Server.Controllers.Common;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Server.Controllers;

/// <summary>
/// Аутентификация и авторизация пользователей
/// </summary>
[Route(ServerRoutes.Controllers.AuthenticationController)]
public sealed class AuthenticationController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Запрос на вход пользователя
    /// </summary>
    [HttpPost(LoginRequest.RouteConstant), AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginRequest.Body body)
    {
        var loginQuery = _mapper.Map<LoginQuery>(body);
        var loginCommandResult = await _sender.Send(loginQuery);

        if (loginCommandResult.IsError) return Problem(loginCommandResult.Errors);
        var authResult = loginCommandResult.Value;

        var authResponse = _mapper.Map<AuthenticationResponse>(authResult);
        return Ok(authResponse);
    }

    /// <summary>
    /// Запрос на регистрацию пользователя
    /// </summary>
    [HttpPost(RegistrationRequest.RouteConstant), AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> Registration([FromBody] RegistrationRequest.Body body)
    {
        var registrationCommand = _mapper.Map<RegistrationCommand>(body);
        var registrationCommandResult = await _sender.Send(registrationCommand);

        if (registrationCommandResult.IsError) return Problem(registrationCommandResult.Errors);
        var authResult = registrationCommandResult.Value;

        var authResponse = _mapper.Map<AuthenticationResponse>(authResult);
        return Ok(authResponse);
    }
}
