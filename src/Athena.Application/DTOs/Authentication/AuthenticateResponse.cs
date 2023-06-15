namespace Athena.Application.DTOs.Authentication;

public class AuthenticateResponse
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public AuthenticateResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}