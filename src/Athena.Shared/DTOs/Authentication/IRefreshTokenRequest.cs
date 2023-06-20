namespace Athena.Shared.DTOs.Authentication;

public interface IRefreshTokenRequest
{
    string RefreshToken { get; set; }
    
    string AccessToken { get; set; }
}