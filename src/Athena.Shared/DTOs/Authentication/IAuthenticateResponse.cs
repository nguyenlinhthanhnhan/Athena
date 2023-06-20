namespace Athena.Shared.DTOs.Authentication;

public interface IAuthenticateResponse
{
    string? AccessToken { get; set; }

    string? RefreshToken { get; set; }
}