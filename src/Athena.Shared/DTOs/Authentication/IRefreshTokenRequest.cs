namespace Athena.Shared.DTOs.Authentication;

public interface IRefreshTokenRequest
{
    string AccessToken { get; set; }
}