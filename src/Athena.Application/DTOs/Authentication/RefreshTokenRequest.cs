namespace Athena.Application.DTOs.Authentication;

public class RefreshTokenRequest
{
    public string AccessToken { get; set; } = null!;
}