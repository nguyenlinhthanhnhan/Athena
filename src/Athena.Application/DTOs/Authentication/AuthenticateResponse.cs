using Athena.Shared.DTOs.Authentication;

namespace Athena.Application.DTOs.Authentication;

public class AuthenticateResponse : IAuthenticateResponse
{
    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public AuthenticateResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}