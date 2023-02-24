using Carbon.Contracts.Routes.Server;
using Carbon.Server.Controllers.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Server.Controllers;

[Route(ServerRoutes.Controllers.ErrorController)]
public sealed class ErrorController : ApiController
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult HandleError()
    {
        return Problem();
    }
}