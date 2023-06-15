using Athena.Application.DTOs.User;
using Athena.Application.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Athena.API.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    public AccountController(IUserService userService) => _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        var createdUser = await _userService.RegisterAsync(model);

        return Ok(createdUser.Id);
    }
}