using Athena.Application.DTOs.Authentication;
using Athena.Application.DTOs.User;

namespace Athena.Application.Identity;

public interface ICustomAuthenticationService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);

    AuthenticateResponse RefreshToken(int userId, string token);
    
    void RevokeToken(int userId);
}