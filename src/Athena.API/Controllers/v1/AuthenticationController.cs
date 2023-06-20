using Athena.API.Helpers;
using Athena.Application.DTOs.Authentication;
using Athena.Application.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Athena.API.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public AuthenticationController(ICustomAuthenticationService customAuthenticationService) =>
        _customAuthenticationService = customAuthenticationService;

    [AllowAnonymous]
    [HttpPost("auth")]
    public IActionResult Authenticate([FromBody] AuthenticateRequest model)
    {
        var response = _customAuthenticationService.Authenticate(model);
        
        return Ok(response);
    }

    /// <summary>
    /// Get new access token
    /// </summary>

    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = _customAuthenticationService.RefreshToken(request);
        return Ok(result);
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("revoke")]
    public IActionResult RevokeToken()
    {
        var userId = HttpContext.GetCurrentUser();

        _customAuthenticationService.RevokeToken(userId);

        return Ok();
    }
}