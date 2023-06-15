using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Athena.API.Helpers;

public static class AuthHelper
{
    /// <summary>
    /// Get the current user id from the request header
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static int GetCurrentUser(this HttpContext context)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId!);
    }
}