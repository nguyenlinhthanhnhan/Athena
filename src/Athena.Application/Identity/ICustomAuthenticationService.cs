using Athena.Application.DTOs.Authentication;
using Athena.Application.DTOs.User;

namespace Athena.Application.Identity;

public interface ICustomAuthenticationService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);

    AuthenticateResponse RefreshToken(RefreshTokenRequest request);
    
    void RevokeToken(int userId);
}