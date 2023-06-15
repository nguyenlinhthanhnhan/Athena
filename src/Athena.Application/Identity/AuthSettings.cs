namespace Athena.Application.Identity;

public class AuthSettings
{
    public string AccessTokenSecret { get; set; } = null!;

    public string AccessTokenExpiration { get; set; } = null!;

    public string RefreshTokenSecret { get; set; } = null!;

    public string RefreshTokenExpiration { get; set; } = null!;
}