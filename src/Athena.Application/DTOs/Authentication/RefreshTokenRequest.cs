using Athena.Shared.DTOs.Authentication;

namespace Athena.Application.DTOs.Authentication;

public class RefreshTokenRequest : IRefreshTokenRequest
{
    public string RefreshToken { get; set; } = null!;
    
    public string AccessToken { get; set; } = null!;
}