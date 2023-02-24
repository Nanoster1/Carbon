using Carbon.Server.Authentication.Configurations;
using Carbon.Server.Authentication.Defaults;
using Carbon.Server.Controllers.Common;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Server.Controllers;

[Route("api/test")]
public sealed class TestController : ApiController
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ISender _sender;

    public TestController(ILoggerFactory loggerFactory, ISender sender)
    {
        _loggerFactory = loggerFactory;
        _sender = sender;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = $"{GoogleAuthenticationConfig.OAuthAuthenticationScheme}")]
    public IActionResult Get()
    {
        return Ok(new
        {
            Id = User.Claims.FirstOrDefault(c => c.Type == GoogleClaimTypes.Id)?.Value,
            Name = User.Claims.FirstOrDefault(x => x.Type == GoogleClaimTypes.Name)?.Value,
            Email = User.Claims.FirstOrDefault(x => x.Type == GoogleClaimTypes.Email)?.Value,
        });
    }

}